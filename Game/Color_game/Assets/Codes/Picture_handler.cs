using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Picture_handler : MonoBehaviour
{

    private Picture_calculator pc;
    public List<Texture2D> picture_list;
    public Texture2D selected_picture;
    public List<GameObject> original_picutres_ui;
    public GameObject picture_ui_s;
    public GameObject picture_ui_d;

    private Texture2D created_picture_statistics = null;
    private Texture2D created_picture_dominants = null;

    public GameObject cube;

    public List<Color> colours = new List<Color>();
    public List<Color> colours_cl = new List<Color>();
    public List<float> colour_number = new List<float>();
    public List<float> colour_number_cl = new List<float>();

    private Thread picture_thread;


    public class Picture_calculator
    {
        private float diff;
        private float diff_kmeans;
        private int clusters;

        public Color[] coloures_in_picture;

        public List<Color> coloures_stat_based = new List<Color>();
        public List<Color> coloures_cluster_based = new List<Color>();
        public List<float> colour_number_stat = new List<float>();
        public List<float> colour_number_cl = new List<float>();
        private Dictionary<int, List<Color>> colours_in_cluster = new Dictionary<int, List<Color>>();

        public Color[,] stat_picture_coloures;
        public Color[,] clust_picture_coloures;

        public Picture_calculator(Color[] coloures_in_picture)
        {
            diff = 0.05f;
            diff_kmeans = 0.001f;
            clusters = 5;
            this.coloures_in_picture = coloures_in_picture;

            stat_picture_coloures = new Color[600,600];
            clust_picture_coloures = new Color[600, 600];
        }

        public void Strat_generation()
        {
            Get_Colour_Statistics();

            Get_dominant_colours_Kmeans();

            Generate_outputs(0);
            Generate_outputs(1);
        }

        // ------------------------------ Statistic functions ------------------------------------------
        #region Stat

        // Get stat for coloures
        private void Get_Colour_Statistics()
        {
            int similar_index = 0;
            bool found_similar;
            for (int i = 0; i < coloures_in_picture.Length; i++)
            {
                found_similar = false;
                float distance = 0;

                for (int k = 0; k < coloures_stat_based.Count; k++)
                {
                    distance = Colour_Distance_with_E(coloures_stat_based[k], coloures_in_picture[i]);
                    if (distance < diff) { found_similar = true; similar_index = k; break; }
                }
                if (!found_similar)
                {
                    coloures_stat_based.Add(coloures_in_picture[i]);
                    colour_number_stat.Add(1);
                }
                else
                {
                    colour_number_stat[similar_index] += 1 - distance;
                }
            }

            Organize_stat_based_colours();
        }

        // Organize the list based on numbers
        private void Organize_stat_based_colours()
        {
            for (int i = 0; i < colour_number_stat.Count - 1; i++)
            {
                float max = 0;
                int index = 0;

                for (int j = i; j < colour_number_stat.Count; j++)
                {
                    if (colour_number_stat[j] > max)
                    {
                        max = colour_number_stat[j];
                        index = j;
                    }
                }

                Color temp_c = coloures_stat_based[i];
                coloures_stat_based[i] = coloures_stat_based[index];
                coloures_stat_based[index] = temp_c;

                float temp_n = colour_number_stat[i];
                colour_number_stat[i] = colour_number_stat[index];
                colour_number_stat[index] = temp_n;
            }
        }

        #endregion
        // ------------------------------ Cluster functions ------------------------------------------
        #region Cluster

        // First cluster centarls
        private void Get_dominant_colours_Kmeans()
        {
            if (clusters == 0) clusters = 5;
            Color[] center_c = new Color[clusters];

            System.Random x, y;
            x = new System.Random();

            // calculate first centrals
            for (int i = 0; i < center_c.Length; i++)
            {
                bool is_ok = true;
                int cycle = 0;
                do
                {
                    center_c[i] = coloures_in_picture[x.Next(0, coloures_in_picture.Length)];

                    for (int j = 0; j < i; j++)
                    {
                        if (Colour_Distance_with_E_W(center_c[i], center_c[j]) < 0.1) is_ok = false;
                    }
                    cycle++;
                }
                while (is_ok == false && cycle < 10);

                colours_in_cluster.Add(i, new List<Color>());
            }

            Calculate_clusters(center_c);
        }

        // Calculate clusters
        private void Calculate_clusters(Color[] center_c)
        {
            float clu_dif = 0;
            do
            {
                clu_dif = 0;

                // clear clusters
                for (int i = 0; i < colours_in_cluster.Count; i++)
                {
                    colours_in_cluster[i].Clear();
                }

                // fill clusters with data
                for (int i = 0; i < coloures_in_picture.Length; i++)
                {
                    int index = 0;
                    float min_dist = 100;
                    for (int k = 0; k < center_c.Length; k++)
                    {
                        float distance = Colour_Distance_with_E_W(center_c[k], coloures_in_picture[i]);
                        if (min_dist > distance)
                        {
                            min_dist = distance;
                            index = k;
                        }
                    }
                    colours_in_cluster[index].Add(coloures_in_picture[i]);
                }

                // calculate new centrals, and how much the centrals moved in distance
                for (int i = 0; i < colours_in_cluster.Count; i++)
                {
                    float r, g, b;
                    r = g = b = 0;

                    if (colours_in_cluster[i].Count == 0) // adds new random cluster central, because old cluster had no data
                    {
                        clu_dif += 1;
                        System.Random x, y;
                        x = new System.Random();

                        bool is_ok = true;
                        int cycle = 0;
                        do
                        {
                            center_c[i] = coloures_in_picture[x.Next(0, coloures_in_picture.Length)];

                            for (int j = 0; j < i; j++)
                            {
                                if (Colour_Distance_with_E_W(center_c[i], center_c[j]) < 0.1) is_ok = false;
                            }
                            cycle++;
                        }
                        while (is_ok == false && cycle < 10);
                    }
                    else // calculate new central if it had any data
                    {
                        for (int j = 0; j < colours_in_cluster[i].Count; j++)
                        {
                            r += colours_in_cluster[i][j].r;
                            g += colours_in_cluster[i][j].g;
                            b += colours_in_cluster[i][j].b;
                        }

                        r = r / colours_in_cluster[i].Count;
                        g = g / colours_in_cluster[i].Count;
                        b = b / colours_in_cluster[i].Count;

                        Color c = new Color(r, g, b);
                        clu_dif += Colour_Distance_with_E_W(c, center_c[i]);
                        center_c[i] = c;
                    }
                }
                clu_dif /= colours_in_cluster.Count;

            } while (clu_dif > diff_kmeans);

            // fill colours list with centrals
            for (int i = 0; i < center_c.Length; i++)
            {
                float min_dist = 10;
                int min_index = 0;

                for (int k = 0; k < colours_in_cluster[i].Count; k++)
                {
                    float dist = Colour_Distance_with_E(center_c[i], colours_in_cluster[i][k]);
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        min_index = k;
                    }
                }

                coloures_cluster_based.Add(colours_in_cluster[i][min_index]);
            }

            Get_Colour_Statistics_for_clusters();
            Organize_colours_in_clusters();
        }

        // Counts how many times the cluster's most dominant colour apppeared in the picture
        private void Get_Colour_Statistics_for_clusters()
        {
            for (int k = 0; k < coloures_cluster_based.Count; k++)
            {
                colour_number_cl.Add(0);
            }

            int similar_index = -1;
            for (int i = 0; i < coloures_in_picture.Length; i++)
            {
                float distance = 0;

                for (int k = 0; k < coloures_cluster_based.Count; k++)
                {
                    distance = Colour_Distance_with_E(coloures_cluster_based[k], coloures_in_picture[i]);
                    if (distance < diff) { similar_index = k; break; }
                }
                if (similar_index != -1)
                {
                    colour_number_cl[similar_index]++;
                }
            }
        }

        // Organise the 5 most dominant coloures based on how many times they appeared
        private void Organize_colours_in_clusters()
        {
            for (int i = 0; i < colour_number_cl.Count; i++)
            {
                float max = 0;
                int index = 0;

                for (int j = i; j < colour_number_cl.Count; j++)
                {
                    if (colour_number_cl[j] > max)
                    {
                        max = colour_number_cl[j];
                        index = j;
                    }
                }

                Color temp_c = coloures_cluster_based[i];
                coloures_cluster_based[i] = coloures_cluster_based[index];
                coloures_cluster_based[index] = temp_c;

                float temp_n = colour_number_cl[i];
                colour_number_cl[i] = colour_number_cl[index];
                colour_number_cl[index] = temp_n;
            }
        }

        #endregion
        // ------------------------------ Distance functions ------------------------------------------
        #region Distance
        private float Colour_Distance(Color first, Color second)
        {
            float r_dist = 0;
            float g_dist = 0;
            float b_dist = 0;

            r_dist = Mathf.Abs(first.r - second.r);
            g_dist = Mathf.Abs(first.g - second.g);
            b_dist = Mathf.Abs(first.b - second.b);


            return ((r_dist + g_dist + b_dist) / 3);
        }

        private float Colour_Distance_with_E(Color first, Color second)
        {
            float dist = 0;

            dist = Mathf.Sqrt(Mathf.Pow((first.r - second.r), 2) + Mathf.Pow((first.g - second.g), 2) + Mathf.Pow((first.b - second.b), 2));

            return dist;
        }

        private float Colour_Distance_with_E_W(Color first, Color second)
        {
            float dist = 0;
            float r = first.r - second.r;
            float g = first.g - second.g;
            float b = first.b - second.b;
            float r_check = (first.r + second.r) / 2;

            if (r_check > 0.5f)
                dist = Mathf.Sqrt((2 * Mathf.Pow((r), 2)) + (4 * Mathf.Pow((g), 2)) + (3 * Mathf.Pow((b), 2)));
            else
                dist = Mathf.Sqrt((3 * Mathf.Pow((r), 2)) + (4 * Mathf.Pow((g), 2)) + (2 * Mathf.Pow((b), 2)));

            return dist;
        }
        #endregion
        // ------------------------------ Output generator functions ------------------------------------------
        private void Generate_outputs(int list_index)
        {
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 600; j++)
                {
                    if (list_index == 0) stat_picture_coloures[i,j] = coloures_stat_based[0];
                    else clust_picture_coloures[i,j] = coloures_cluster_based[0];
                }
            }


            // first diamond looking part 
            int k = 300;
            for (int i = 0; i < 300; i++)
            {
                for (int j = k; j <= (300 + i); j++)
                {
                    if (list_index == 0) stat_picture_coloures[i,j] = coloures_stat_based[1];
                    else clust_picture_coloures[i,j] = coloures_cluster_based[1];
                }
                k--;
            }


            k = 300;
            for (int i = 599; i >= 300; i--)
            {
                for (int j = k; j <= (300 + (599 - i)); j++)
                {
                    //Debug.Log("i:" + i.ToString() + "j:" + j.ToString());
                    if (list_index == 0) stat_picture_coloures[i,j] = coloures_stat_based[1];
                    else clust_picture_coloures[i,j] = coloures_cluster_based[1];

                }
                k--;
            }
            // first diamond looking part end

            for (int i = 150; i < 450; i++)
            {
                for (int j = 150; j < 450; j++)
                {
                    if (list_index == 0) stat_picture_coloures[i,j] = coloures_stat_based[2];
                    else clust_picture_coloures[i,j] = coloures_cluster_based[2];
                }
            }


            // second diamond looking part 
            k = 300;
            for (int i = 150; i < 300; i++)
            {
                for (int j = k; j <= (300 + (i - 150)); j++)
                {
                    if (list_index == 0) stat_picture_coloures[i,j] = coloures_stat_based[3];
                    else clust_picture_coloures[i,j] = coloures_cluster_based[3];
                }
                k--;
            }


            k = 300;
            for (int i = 450; i >= 300; i--)
            {
                for (int j = k; j <= (300 + (450 - i)); j++)
                {
                    if (list_index == 0) stat_picture_coloures[i,j] = coloures_stat_based[3];
                    else clust_picture_coloures[i,j] = coloures_cluster_based[3];
                }
                k--;
            }

            // second diamond looking part end

            for (int i = 225; i < 375; i++)
            {
                for (int j = 225; j < 375; j++)
                {
                    if (list_index == 0) stat_picture_coloures[i,j] = coloures_stat_based[4];
                    else clust_picture_coloures[i,j] = coloures_cluster_based[4];
                }
            }
        }
    }

    private void Create_picture()
    {
        Texture2D created_picture = new Texture2D(600, 600);
        Texture2D created_picture2 = new Texture2D(600, 600);

        for (int i = 0; i < 600; i++)
        {
            for (int j = 0; j < 600; j++)
            {
                created_picture.SetPixel(i, j, pc.stat_picture_coloures[i, j]);
                created_picture2.SetPixel(i, j, pc.clust_picture_coloures[i, j]);
            }
        }

        created_picture.Apply();
        created_picture2.Apply();

        created_picture_statistics = created_picture;
        created_picture_dominants = created_picture2;
    }

    private void Start()
    {
        if (selected_picture == null)
        {
            System.Random r = new System.Random();
            int num = r.Next(0, picture_list.Count - 1);
            selected_picture = picture_list[num];
        }

        pc = new Picture_calculator(selected_picture.GetPixels());

        foreach (var item in original_picutres_ui)
        {
            item.GetComponent<RawImage>().texture = selected_picture;
        }

        picture_thread = new Thread(pc.Strat_generation);

        picture_thread.Start();
    }

    private void Update()
    {
        if (picture_thread != null)
        {
            if (!picture_thread.IsAlive)
            {
                Create_picture();
            }

            if (created_picture_dominants != null && created_picture_statistics != null)
            {
                picture_ui_d.GetComponent<RawImage>().texture = created_picture_dominants;
                picture_ui_s.GetComponent<RawImage>().texture = created_picture_statistics;
                picture_thread = null;
            }
        }
    }

    /*

    public Texture2D selected_picture;
    public GameObject colour_ui;
    public GameObject picture_ui_s;
    public GameObject picture_ui_d;

    public float diff;
    public float diff_kmeans;
    public int clusters;

    public List<Color> colours = new List<Color>();
    public List<Color> colours_cl = new List<Color>();
    public List<float> colour_number = new List<float>();
    public List<float> colour_number_cl = new List<float>();

    private Color[] pixel_c;

    private Texture2D created_picture_statistics;
    private Texture2D created_picture_dominants;
    private Dictionary<int, List<Color>> cluster_colours = new Dictionary<int, List<Color>>();

    private void Start()
    {
        pixel_c = selected_picture.GetPixels();

        // create picture from statistics 

        Get_Colour_Statistics();

        Organize_colours();

        created_picture_statistics = new Texture2D(600, 600);

        created_picture_statistics = Create_picture(600, 0);

        colour_ui.GetComponent<RawImage>().texture = selected_picture;
        picture_ui_s.GetComponent<RawImage>().texture = created_picture_statistics;

        // create picture from dominant colours   
        Get_dominant_colours_Kmeans();
    }

    private void Get_dominant_colours_Kmeans()
    {
        created_picture_dominants = new Texture2D(600, 600);

        if (clusters == 0) clusters = 5;
        Color[] center_c = new Color[clusters];

        System.Random x, y;
        x = new System.Random();
        y = new System.Random();

        for (int i = 0; i < center_c.Length; i++)
        {
            bool is_ok = true;
            int cycle = 0;
            do
            {
                center_c[i] = pixel_c[x.Next(0, pixel_c.Length)];

                for (int j = 0; j < i; j++)
                {
                    if (Colour_Distance_with_E_W(center_c[i], center_c[j]) < 0.1) is_ok = false;
                }
                cycle++;
            }
            while (is_ok == false && cycle < 10);

            cluster_colours.Add(i, new List<Color>());
        }

        Calculate_clusters(center_c);

        created_picture_dominants = Create_picture(600, 1);

        picture_ui_d.GetComponent<RawImage>().texture = created_picture_dominants;
    }

    private void Calculate_clusters(Color[] center_c)
    {
        float clu_dif = 0;
        do
        {
            clu_dif = 0;

            // clear clusters
            for (int i = 0; i < cluster_colours.Count; i++)
            {
                cluster_colours[i].Clear();
            }

            // fill clusters with data
            for (int i = 0; i < pixel_c.Length; i++)
            {
                Color c = pixel_c[i];
                int index = 0;
                float min_dist = 100;
                for (int k = 0; k < center_c.Length; k++)
                {
                    float distance = Colour_Distance_with_E_W(center_c[k], c);
                    if (min_dist > distance)
                    {
                        min_dist = distance;
                        index = k;
                    }
                }
                cluster_colours[index].Add(c);
            }

            // calculate new centrals
            for (int i = 0; i < cluster_colours.Count; i++)
            {
                float r, g, b;
                r = g = b = 0;

                if (cluster_colours[i].Count == 0)
                {
                    clu_dif += 1;
                    System.Random x, y;
                    x = new System.Random();
                    y = new System.Random();

                    bool is_ok = true;
                    int cycle = 0;
                    do
                    {
                        center_c[i] = pixel_c[x.Next(0, pixel_c.Length)];

                        for (int j = 0; j < i; j++)
                        {
                            if (Colour_Distance_with_E_W(center_c[i], center_c[j]) < 0.1) is_ok = false;
                        }
                        cycle++;
                    }
                    while (is_ok == false && cycle < 10);

                }
                else
                {
                    for (int j = 0; j < cluster_colours[i].Count; j++)
                    {
                        r += cluster_colours[i][j].r;
                        g += cluster_colours[i][j].g;
                        b += cluster_colours[i][j].b;
                    }

                    r = r / cluster_colours[i].Count;
                    g = g / cluster_colours[i].Count;
                    b = b / cluster_colours[i].Count;

                    Color c = new Color(r, g, b);
                    clu_dif += Colour_Distance_with_E_W(c, center_c[i]);
                    center_c[i] = c;
                }
            }
            clu_dif /= cluster_colours.Count;

        } while (clu_dif > diff_kmeans);

        // fill colours list with centrals
        for (int i = 0; i < center_c.Length; i++)
        {
            float min_dist = 10;
            int min_index = 0;

            for (int k = 0; k < cluster_colours[i].Count; k++)
            {
                float dist = Colour_Distance_with_E(center_c[i], cluster_colours[i][k]);
                if (dist < min_dist)
                {
                    min_dist = dist;
                    min_index = k;
                }
            }

            Color temp = cluster_colours[i][0];
            cluster_colours[i][0] = cluster_colours[i][min_index];
            cluster_colours[i][min_index] = temp;

            colours_cl.Add(cluster_colours[i][0]);
        }


        Get_Colour_Statistics_for_clusters();
        Organize_colours_in_clusters();
    }

    private void Get_Colour_Statistics_for_clusters()
    {

        for (int k = 0; k < colours_cl.Count; k++)
        {
            colour_number_cl.Add(0);
        }

        int similar_index = -1;
        for (int i = 0; i < pixel_c.Length; i++)
        {
            Color c = pixel_c[i];
            float distance = 0;

            for (int k = 0; k < colours_cl.Count; k++)
            {
                distance = Colour_Distance_with_E(colours_cl[k], c);
                if (distance < diff) { similar_index = k; break; }
            }
            if (similar_index != -1)
            {
                colour_number_cl[similar_index]++;
            }
        }
    }

    private void Organize_colours_in_clusters()
    {
        for (int i = 0; i < colour_number_cl.Count; i++)
        {
            float max = 0;
            int index = 0;

            for (int j = i; j < colour_number_cl.Count; j++)
            {
                if (colour_number_cl[j] > max)
                {
                    max = colour_number_cl[j];
                    index = j;
                }
            }

            Color temp_c = colours_cl[i];
            colours_cl[i] = colours_cl[index];
            colours_cl[index] = temp_c;

            float temp_n = colour_number_cl[i];
            colour_number_cl[i] = colour_number_cl[index];
            colour_number_cl[index] = temp_n;
        }
    }

    private void Get_Colour_Statistics()
    {
        int similar_index = 0;
        bool found_similar;
        for (int i = 0; i < pixel_c.Length; i++)
        {
            Color c = pixel_c[i];
            found_similar = false;
            float distance = 0;

            for (int k = 0; k < colours.Count; k++)
            {
                //if (Colour_Distance(colours[k], c) < diff) { found_similar = true; similar_index = k; break; }
                distance = Colour_Distance_with_E(colours[k], c);
                if (distance < diff) { found_similar = true; similar_index = k; break; } // 0.05 was the best so far
            }
            if (!found_similar)
            {
                colours.Add(c);
                colour_number.Add(1);
            }
            else
            {
                colour_number[similar_index] += 1 - distance;
            }
        }
    }

    private float Colour_Distance(Color first, Color second)
    {
        float r_dist = 0;
        float g_dist = 0;
        float b_dist = 0;

        r_dist = Mathf.Abs(first.r - second.r);
        g_dist = Mathf.Abs(first.g - second.g);
        b_dist = Mathf.Abs(first.b - second.b);


        return ((r_dist + g_dist + b_dist) / 3);
    }

    private float Colour_Distance_with_E(Color first, Color second)
    {
        float dist = 0;

        dist = Mathf.Sqrt(Mathf.Pow((first.r - second.r), 2) + Mathf.Pow((first.g - second.g), 2) + Mathf.Pow((first.b - second.b), 2));

        return dist;
    }

    private float Colour_Distance_with_E_W(Color first, Color second)
    {
        float dist = 0;
        float r = first.r - second.r;
        float g = first.g - second.g;
        float b = first.b - second.b;
        float r_check = (first.r + second.r) / 2;

        if (r_check > 0.5f)
            dist = Mathf.Sqrt((2 * Mathf.Pow((r), 2)) + (4 * Mathf.Pow((g), 2)) + (3 * Mathf.Pow((b), 2)));
        else
            dist = Mathf.Sqrt((3 * Mathf.Pow((r), 2)) + (4 * Mathf.Pow((g), 2)) + (2 * Mathf.Pow((b), 2)));

        return dist;
    }

    private void Organize_colours()
    {
        for (int i = 0; i < colour_number.Count - 1; i++)
        {
            float max = 0;
            int index = 0;

            for (int j = i; j < colour_number.Count; j++)
            {
                if (colour_number[j] > max)
                {
                    max = colour_number[j];
                    index = j;
                }
            }

            Color temp_c = colours[i];
            colours[i] = colours[index];
            colours[index] = temp_c;

            float temp_n = colour_number[i];
            colour_number[i] = colour_number[index];
            colour_number[index] = temp_n;
        }
    }

    private Texture2D Create_picture(int size, int list_index)
    {
        Texture2D created_picture = new Texture2D(size, size);
        for (int i = 0; i < 600; i++)
        {
            for (int j = 0; j < 600; j++)
            {
                if (list_index == 0) created_picture.SetPixel(i, j, colours[0]);
                else created_picture.SetPixel(i, j, colours_cl[0]);
            }
        }

        // first diamond looking part 
        int k = 300;
        for (int i = 0; i < 300; i++)
        {
            for (int j = k; j <= (300 + i); j++)
            {
                if (list_index == 0) created_picture.SetPixel(i, j, colours[1]);
                else created_picture.SetPixel(i, j, colours_cl[1]);
            }
            k--;
        }

        k = 300;
        for (int i = 600; i >= 300; i--)
        {
            for (int j = k; j <= (300 + (600 - i)); j++)
            {
                if (list_index == 0) created_picture.SetPixel(i, j, colours[1]);
                else created_picture.SetPixel(i, j, colours_cl[1]);
            }
            k--;
        }
        // first diamond looking part end

        for (int i = 150; i < 450; i++)
        {
            for (int j = 150; j < 450; j++)
            {
                if (list_index == 0)
                    created_picture.SetPixel(i, j, colours[2]);
                else
                    created_picture.SetPixel(i, j, colours_cl[2]);
            }
        }

        // second diamond looking part 
        k = 300;
        for (int i = 150; i < 300; i++)
        {
            for (int j = k; j <= (300 + (i - 150)); j++)
            {
                if (list_index == 0)
                    created_picture.SetPixel(i, j, colours[3]);
                else
                    created_picture.SetPixel(i, j, colours_cl[3]);
            }
            k--;
        }

        k = 300;
        for (int i = 450; i >= 300; i--)
        {
            for (int j = k; j <= (300 + (450 - i)); j++)
            {
                if (list_index == 0)
                    created_picture.SetPixel(i, j, colours[3]);
                else
                    created_picture.SetPixel(i, j, colours_cl[3]);
            }
            k--;
        }
        // second diamond looking part end

        for (int i = 225; i < 375; i++)
        {
            for (int j = 225; j < 375; j++)
            {
                if (list_index == 0)
                    created_picture.SetPixel(i, j, colours[4]);
                else
                    created_picture.SetPixel(i, j, colours_cl[4]);
            }
        }

        created_picture.Apply();
        return created_picture;
    }

    */
}
