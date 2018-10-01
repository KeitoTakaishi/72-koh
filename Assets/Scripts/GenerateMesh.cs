using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MainScene
{
    public class GenerateMesh : MonoBehaviour
    {
        private List<string> tempData;
        private CreateCSV _CreateCSV;
        private Mesh mesh;
        private MeshFilter mf;

        void Start()
        {
            tempData = new List<string>();
            _CreateCSV = GameObject.Find("CSV").GetComponent<CreateCSV>();
            _CreateCSV.ReadCSV(ref tempData);
            ModifyData(ref tempData);

            mesh = new Mesh();
            mesh = CreateMesh(ref mesh, tempData);
            mf = GetComponent<MeshFilter>();
            mf.sharedMesh = mesh;
            mf.mesh.SetIndices(mf.mesh.GetIndices(0), MeshTopology.LineStrip, 0);
        }

        void Update()
        {
            this.transform.position += new Vector3(Time.deltaTime*4.0f, 0.0f, 0.0f);
        }

        private void ModifyData(ref List<string>data){
            string year,month,_date;

            string date;
            int dateCount = 0;
            int CurSeason = 0;
            const int num = 72;
            List<float>[] temp = new List<float>[num];


            for (int i = 0; i < data.Count; i+=2){
                year = data[i].Split('/')[0];
                month = data[i].Split('/')[1];
                _date = data[i].Split('/')[2];

                //うるう年の振り分け
                if(int.Parse(year) % 4 == 0){
                    
                    if (dateCount == 366)
                    {
                        temp[CurSeason].Add(float.Parse(data[i + 1]));
                        Debug.Log(data[i]);
                        dateCount = 0;
                        CurSeason = 0;
                    }
                    else
                    {
                        if (int.Parse(month) != 2 && int.Parse(_date) != 29)
                        {
                            temp[CurSeason].Add(float.Parse(data[i + 1]));
                        }
                    }
                    if (int.Parse(month) != 2 && int.Parse(_date) != 29)
                    {
                        dateCount++;
                    }
                    if (dateCount % 5 == 0)
                    {
                        CurSeason++;
                    }

                }else{
                    
                    if (dateCount == 365)
                    {
                        temp[CurSeason].Add(float.Parse(data[i + 1]));
                        Debug.Log(data[i]);
                        dateCount = 0;
                        CurSeason = 0;
                    }else {
                        temp[CurSeason].Add(float.Parse(data[i + 1]));
                    }
                    dateCount++;
                    if(dateCount % 5 == 0){
                        CurSeason++; 
                    }

                }
            }
        }

        private Mesh CreateMesh(ref Mesh mesh, List<string>data){
            List<Vector3> vertex = new List<Vector3>();
            List<int> index = new List<int>();
            int Date = data.Count/2;
            int ArrIndex = 0;
            for (int i = 0; i < Date*2.0; i+=2){
                
                vertex.Add(new Vector3((-Date/2 + ArrIndex)*0.2f, float.Parse(data[i+1]), 0.0f));
                index.Add(i / 2);
                ArrIndex++;
            }
           
            mesh.SetVertices(vertex);
            mesh.SetTriangles(index.ToArray(), 0);
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}
