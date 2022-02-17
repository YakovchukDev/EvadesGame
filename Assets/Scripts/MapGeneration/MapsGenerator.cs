using System.Collections.Generic;
using UnityEngine;

namespace Maps
{
    public class MapsGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _smallEnemy;

        public int Complexity = 1;
        public int Length;
        public int Branchs = 1;
        public int StepX = 52;
        public int StepZ = 13;
        [SerializeField]
        private List<Room> Arens;
        [SerializeField]
        private List<Room> Corridors;
        [SerializeField]
        private GameObject SaveZone;
        [SerializeField]
        private GameObject SaveZoneStart;
        [SerializeField]
        private GameObject SaveZoneOfEnd;

        [SerializeField]
        private GameObject NotSaveZone;
        public int SizeSaveZoneХ = 13;

        private Room[,] Maps;

        public void Start()
        {
            Maps = new Room [Length, Branchs*2 + 1];

            System.Random r = new System.Random();
            int quantityBranchs = 0;
            //generate arens
            Maps[0, Branchs] = Arens[0];
            for(int i = 1; i < Length - 1; i++)
            {
                List<Room> SelectRooms = new List<Room>();
                for(int ii = 0; ii < Arens.Count; ii++)
                {
                    if(Maps[i-1, Branchs].SaveRight && Arens[ii].SaveLeft)
                    {
                        if((Arens[ii].ExitUp || Arens[ii].ExitDown))
                        {
                            if(checkVertical(i, quantityBranchs)) 
                            {
                                SelectRooms.Add(Arens[ii]);
                            }
                        }
                        else
                        {
                            SelectRooms.Add(Arens[ii]);
                        }
                    }
                    else if(!Maps[i-1, Branchs].SaveRight && !Arens[ii].SaveLeft)
                    {
                        if((Arens[ii].ExitUp || Arens[ii].ExitDown))
                        {
                            if(checkVertical(i, quantityBranchs)) 
                            {
                                SelectRooms.Add(Arens[ii]);
                            }
                        }
                        else
                        {
                            SelectRooms.Add(Arens[ii]);
                        }
                    }
                } 

                int nextNumber = r.Next(0, SelectRooms.Count);
                if((SelectRooms[nextNumber].ExitUp || SelectRooms[nextNumber].ExitDown))
                {
                    if(Length % 10 < 5 || quantityBranchs > 2)
                    {
                        nextNumber = r.Next(0, SelectRooms.Count);
                    }
                    if(Length % 10 < 2 || quantityBranchs > 1)
                    {
                        nextNumber = r.Next(0, SelectRooms.Count);
                    }
                }
                if(SelectRooms[nextNumber].ExitUp || SelectRooms[nextNumber].ExitDown)
                {
                    quantityBranchs++;
                }

                /*
                int quantityBranchsInChunk = Complexity / 2 + 1;
                for(int j = 10, n = 3; j < 50; j += 10, n++)
                {
                    if(i < j && quantityBranchsInChunk > n)
                    {
                        quantityBranchsInChunk = n;
                    } 
                }
                Debug.Log($"{i}, {quantityBranchs}, {quantityBranchsInChunk}");
                */
                

                if(i % 10 == 0) quantityBranchs = 0;

                Maps[i, Branchs] = SelectRooms[nextNumber];
            }
            Maps[Length - 1, Branchs] = Arens[0];
            //generate lower corridors
            for(int i = 1; i < Length; i++)
            {
                if(Maps[i, Branchs].ExitDown)
                {
                    if(i + 2 < Length - 1 && Maps[i + 2, Branchs].ExitDown)
                    {
                        List<Room> leftCorridors = new List<Room>();
                        List<Room> rightCorridors = new List<Room>();
                        List<Room> centralCorridors = new List<Room>();

                        for(int ii = 0; ii < Corridors.Count; ii++)
                        {
                            if(Corridors[ii].ExitUp)
                            {
                                if(Corridors[ii].ExitRight && !Corridors[ii].ExitLeft) 
                                {
                                    leftCorridors.Add(Corridors[ii]);
                                }
                                else if(Corridors[ii].ExitLeft && !Corridors[ii].ExitRight)
                                {
                                    rightCorridors.Add(Corridors[ii]);
                                }
                            }
                            
                            if(Corridors[ii].ExitLeft && Corridors[ii].ExitRight)
                            {
                                if((Maps[i + 1, Branchs].ExitDown && Corridors[ii].ExitUp && !Corridors[ii].ExitDown) || (!Maps[i + 1, Branchs].ExitDown && !Corridors[ii].ExitUp && !Corridors[ii].ExitDown))
                                {
                                    centralCorridors.Add(Corridors[ii]);
                                }
                            }
                        }

                        int indexLeftCorridor = r.Next(0, leftCorridors.Count);
                        int indexCentralCorridor = r.Next(0, centralCorridors.Count);
                        int indexRightCorridor = r.Next(0, rightCorridors.Count);

                        Maps[i, Branchs - 1] = leftCorridors[indexLeftCorridor];
                        Maps[i + 1, Branchs - 1] = centralCorridors[indexCentralCorridor];
                        Maps[i + 2, Branchs - 1] = rightCorridors[indexRightCorridor];
                        i += 2;
                    }
                    else if(i + 1 < Length - 1 && Maps[i + 1, Branchs].ExitDown)
                    {
                        List<Room> leftCorridors = new List<Room>();
                        List<Room> rightCorridors = new List<Room>();

                        for(int ii = 0; ii < Corridors.Count; ii++)
                        {
                            if(Corridors[ii].ExitUp)
                            {
                                if(Corridors[ii].ExitRight && !Corridors[ii].ExitLeft) 
                                {
                                    leftCorridors.Add(Corridors[ii]);
                                }
                                else if(Corridors[ii].ExitLeft && !Corridors[ii].ExitRight)
                                {
                                    rightCorridors.Add(Corridors[ii]);
                                }
                            }
                        }

                        int indexLeftCorridor = r.Next(0, leftCorridors.Count);
                        int indexRightCorridor = r.Next(0, rightCorridors.Count);

                        Maps[i, Branchs - 1] = leftCorridors[indexLeftCorridor];
                        Maps[i + 1, Branchs - 1] = rightCorridors[indexRightCorridor];
                        i += 1;
                    }
                    else
                    {
                        List<Room> oneCorridors = new List<Room>();

                        for(int ii = 0; ii < Corridors.Count; ii++)
                        {
                            if(!Corridors[ii].ExitDown && Corridors[ii].ExitUp && !Corridors[ii].ExitLeft && !Corridors[ii].ExitRight)
                            {
                                oneCorridors.Add(Corridors[ii]);
                            }
                        }

                        int index = r.Next(0, oneCorridors.Count); 
                        Maps[i, Branchs - 1] = oneCorridors[index];
                    }
                }
            }
            //generate upper corridors
            for(int i = 1; i < Length; i++)
            {
                if(Maps[i, Branchs].ExitUp)
                {
                    
                    if(i + 2 < Length - 1 && Maps[i + 2, Branchs].ExitUp)
                    {
                        List<Room> leftCorridors = new List<Room>();
                        List<Room> rightCorridors = new List<Room>();
                        List<Room> centralCorridors = new List<Room>();

                        for(int ii = 0; ii < Corridors.Count; ii++)
                        {
                            if(Corridors[ii].ExitDown)
                            {
                                if(Corridors[ii].ExitRight && !Corridors[ii].ExitLeft) 
                                {
                                    leftCorridors.Add(Corridors[ii]);
                                }
                                else if(Corridors[ii].ExitLeft && !Corridors[ii].ExitRight)
                                {
                                    rightCorridors.Add(Corridors[ii]);
                                }
                            }
                            
                            if(Corridors[ii].ExitLeft && Corridors[ii].ExitRight)
                            {
                                if((Maps[i + 1, Branchs].ExitUp && Corridors[ii].ExitDown && !Corridors[ii].ExitUp) || (!Maps[i + 1, Branchs].ExitUp && !Corridors[ii].ExitDown && !Corridors[ii].ExitUp))
                                {
                                    centralCorridors.Add(Corridors[ii]);
                                }
                            }
                        }

                        int indexLeftCorridor = r.Next(0, leftCorridors.Count);
                        int indexRightCorridor = r.Next(0, rightCorridors.Count);
                        int indexCentralCorridor = r.Next(0, centralCorridors.Count);

                        Maps[i, Branchs + 1] = leftCorridors[indexLeftCorridor];
                        Maps[i + 1, Branchs + 1] = centralCorridors[indexCentralCorridor];
                        Maps[i + 2, Branchs + 1] = rightCorridors[indexRightCorridor];
                        i += 2;
                    }
                    else if(i + 1 < Length - 1 && Maps[i + 1, Branchs].ExitUp)
                    {
                        List<Room> leftCorridors = new List<Room>();
                        List<Room> rightCorridors = new List<Room>();

                        for(int ii = 0; ii < Corridors.Count; ii++)
                        {
                            if(Corridors[ii].ExitDown)
                            {
                                if(Corridors[ii].ExitRight && !Corridors[ii].ExitLeft) 
                                {
                                    leftCorridors.Add(Corridors[ii]);
                                }
                                else if(Corridors[ii].ExitLeft && !Corridors[ii].ExitRight)
                                {
                                    rightCorridors.Add(Corridors[ii]);
                                }
                            }
                        }

                        int indexLeftCorridor = r.Next(0, leftCorridors.Count);
                        int indexRightCorridor = r.Next(0, rightCorridors.Count);

                        Maps[i, Branchs + 1] = leftCorridors[indexLeftCorridor];
                        Maps[i + 1, Branchs + 1] = rightCorridors[indexRightCorridor];
                        i += 1;
                    }
                    else
                    {
                        List<Room> oneCorridors = new List<Room>();

                        for(int ii = 0; ii < Corridors.Count; ii++)
                        {
                            if(!Corridors[ii].ExitUp && Corridors[ii].ExitDown && !Corridors[ii].ExitLeft && !Corridors[ii].ExitRight)
                            {
                                oneCorridors.Add(Corridors[ii]);
                            }
                        }

                        int index = r.Next(0, oneCorridors.Count); 
                        Maps[i, Branchs + 1] = oneCorridors[index];
                    }
                }
            }


            //Show map
            Instantiate(SaveZoneStart, new Vector3(-StepX/2 + -SizeSaveZoneХ/2, 0, 13), Quaternion.identity);
            for(int i = 0; i < Length; i++)
            {
                for(int j = 0; j < Branchs * 2 + 1; j++)
                {
                    if( Maps[i, j] != null)
                    {
                        Maps[i, j].name = $"{i} Arena";
                        Instantiate(Maps[i, j], new Vector3(StepX * i + SizeSaveZoneХ * i, 0, StepZ * j), Quaternion.identity);
                        if(i < Length - 1)
                        {
                            if(j == Branchs)
                            {
                                Instantiate(SaveZone, new Vector3(StepX / 2f + SizeSaveZoneХ/2f + StepX * i + SizeSaveZoneХ * i, 0, StepZ * j), Quaternion.identity);
                            }
                            else if(Maps[i, j].ExitRight)
                            {
                                Instantiate(NotSaveZone, new Vector3(StepX / 2f + SizeSaveZoneХ/2f + StepX * i + SizeSaveZoneХ * i, 0, StepZ * j), Quaternion.identity);
                            }
                        }
                    }
                }
            }
            Instantiate(SaveZoneOfEnd, new Vector3((StepX * Length + SizeSaveZoneХ * Length - 1) + (-StepX/2 + -SizeSaveZoneХ/2), 0, 13), Quaternion.identity);
            //Show Enemy
            for(int i = 0; i < Length; i++)
            {
                //10000% нужно переделать
                foreach (var enemy in _smallEnemy)
                {
                    Instantiate(enemy, new Vector3(Random.Range((StepX * i + SizeSaveZoneХ * i) - (StepX / 2) + 2, (StepX * (i + 1) + SizeSaveZoneХ * i) - (StepX / 2) - 2), 1f, Random.Range(8.0f, 18.0f)), Quaternion.identity);
                    enemy.transform.localScale = new Vector3(1F, 1F, 1F);
                }
            }
        }

        private bool checkVertical(int indexLength, int quantityBranchs)
        {
            int quantityBranchsInChunk = Complexity / 2 + 1;
            
            for(int i = 10, n = 2; i < 50; i += 10, n++)
            {
                if(indexLength < i && quantityBranchsInChunk > n)
                {
                    quantityBranchsInChunk = n;
                } 
            }

            if(quantityBranchs < quantityBranchsInChunk)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

