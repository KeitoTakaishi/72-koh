/*
 * 72候用のデータ配列を格納するスクリプト
 * 5日x10年
 */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Services;
using UnityEngine;


namespace MainScene
{
    [RequireComponent(typeof(MeshFilter))]

    public class TempDataVisual : MonoBehaviour
    {
        public GameObject CSV;
        public GameObject viewController;
        public GameObject histogram;

        private ViewTextController _viewTextController;
        private List < string > _ModifyData;
        private int[] _CycleData;
        private CreateCSV _CreateCSV;
        private CreateDBFromCSV _CDBfromCSV;
        private const int num = 72;
        private List < float >[] _tempData;
        private Mesh _mesh;
        private MeshFilter _mf;
        private GameObject[] _histograms;

        void Start()
        {
            initDataSet();
            GenerateHistogram(_CDBfromCSV.OrderdTempData[0]);

            /*
            initMesh();
            _mesh = CreateMesh(_mesh, _CDBfromCSV.OrderdTempData[0]);
            _mf.sharedMesh = _mesh;
           _mf.mesh.SetIndices(_mf.mesh.GetIndices(0), MeshTopology.LineStrip, 0);
           */
        }

        void Update()
        {
            this.transform.Rotate(new Vector3(0.0f, Time.deltaTime*5.0f, 0.0f));
            
            
            if (_viewTextController.OscId > 0 && _viewTextController.IsPush)
            {
                var id = _viewTextController.OscId-1;
//                _mesh = CreateMesh(_mesh, _CDBfromCSV.OrderdTempData[id]);
//                SetMeshFilter(_mf, _mesh);

                if (_histograms != null)
                {
                    DestroyHistgram();
                }
                GenerateHistogram(_CDBfromCSV.OrderdTempData[id]);
//                
            }
        }


        #region privatefunc

        void initDataSet()
        {
            //cycle日数をセット
            /*
            _CycleData = new int[num];
            InitCycleData(_CycleData);
            _ModifyData = new List<string>();
            CSV = CSV.GetComponent<CSV>();
            CSV.LoadFile(ref _ModifyData, "ModifyData");
            _tempData = new List<float>[num];
            for (int i = 0; i < num; i++){
                _tempData[i] = new List<float>();
            }
            */
            _CDBfromCSV = CSV.GetComponent < CreateDBFromCSV >();
            _viewTextController = viewController.GetComponent < ViewTextController >();
        }

        private void InitCycleData(int[] dates)
        {
            for (int i = 0; i < 72; i++)
            {
                dates[i] = 5;
            }

            dates[0] = 4;
            dates[27] = 6;
            dates[30] = 6;
            dates[34] = 6;
            dates[39] = 6;
            dates[40] = 6;
            dates[42] = 4;
            dates[43] = 6;
            dates[48] = 6;
            dates[68] = 4;
            dates[69] = 6;
        }

        //csvから72候型のデータを作成
        /*
        private void SevrntyTwoDataBase(List<string> data, ref List<float>[] temp){
            int CurSeason;//71まで回ったら初期化
            int CurDate;//3
            CurSeason = 0;
            CurDate = 0;


            //Debug.Log("count;" + data.Count);

            for (int i = 0; i < data.Count;)
            {
                if (int.Parse(data[i].Split(':')[0]) % 4 != 0)
                {
                    for (int j = 0; j < _CycleData[CurSeason]; j++)
                    {
                        var t = float.Parse(data[i].Split(':')[1]);
                        _tempData[CurSeason].Add(t);
                        i++;
                    }
                    CurSeason += 1;
                    if (CurSeason > 71)
                    {
                        CurSeason = 0;
                    }
                }else{
                    if (CurSeason != 11){
                        for (int j = 0; j < _CycleData[CurSeason]; j++)
                        {
                            var t = float.Parse(data[i].Split(':')[1]);
                            _tempData[CurSeason].Add(t);
                            i++;
                        }
                    }else{
                        for (int j = 0; j < _CycleData[CurSeason]+1; j++)
                        {
                            var t = float.Parse(data[i].Split(':')[1]);
                            _tempData[CurSeason].Add(t);
                            i++;
                        }
                    }
                    CurSeason += 1;
                    if (CurSeason > 71)
                    {
                        CurSeason = 0;
                    }
                }
            }
        } 

*/

        void initMesh()
        {
            _mesh = new Mesh();
            _mf = GetComponent < MeshFilter >();
        }


        private Mesh CreateMesh(Mesh mesh, List < float > data)
        {
            _mesh = new Mesh();
            _mf = GetComponent < MeshFilter >();
            var _vertex = new List < Vector3 >();
            var _index = new List < int >();
            var _num = data.Count;
            var _arrIndex = 0;
            var _stepSize = 0.5f;

            for (int i = 0; i < _num; i++)
            {
                _vertex.Add(new Vector3((-_num / 2 + _arrIndex) * _stepSize, data[i], 0.0f));
                _index.Add(i);
                _arrIndex++;
            }

            mesh.SetVertices(_vertex);
            mesh.SetTriangles(_index.ToArray(), 0);
            mesh.RecalculateNormals();

            return mesh;
        }

        private void SetMeshFilter(MeshFilter mf, Mesh mesh)
        {
            _mf.sharedMesh = _mesh;
            _mf.mesh.SetIndices(_mf.mesh.GetIndices(0), MeshTopology.LineStrip, 0);
        }


        private void GenerateHistogram(List < float > data)
        {
            var _dataNum = data.Count;
            _histograms = new GameObject[_dataNum];
            var _radius = 45.0f;
            var _stepDeg = 360.0f / _dataNum;
            
            
            for (int i = 0; i < _dataNum; i++)
            {
                var _x = _radius * Mathf.Cos(_stepDeg * i * Mathf.Deg2Rad);
                var _z = _radius * Mathf.Sin(_stepDeg * i * Mathf.Deg2Rad);
                 
//                _histograms[i] = Instantiate(histogram, new Vector3((-_dataNum / 2 + i) * 1f, 0.0f, 0.0f),
//                    Quaternion.identity);
                _histograms[i] = Instantiate(histogram, new Vector3(_x, 0.0f, _z), Quaternion.Euler(0.0f, -1.0f*_stepDeg*i, 0.0f));
                
                _histograms[i].transform.localScale = new Vector3(1.0f, data[i], 1.0f);
                _histograms[i].transform.SetParent(this.gameObject.transform);
                
            }
        }

        private void DestroyHistgram()
        {
            for (int i = 0; i < _histograms.Length; i++)
            {
                Destroy(_histograms[i]);
            }
        }

        #endregion
    }
}