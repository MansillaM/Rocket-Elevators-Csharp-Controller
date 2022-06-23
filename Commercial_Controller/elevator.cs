using System.Threading;
using System.Collections.Generic;
using System;

namespace Commercial_Controller
{
    public class Elevator
    {
        public int ID = 1;
        public string status;
        public int amountOfFloors;
        public int currentFloor;
        public List<int> floorRequestsList;
        public List<int> completedRequestsList;
        public string direction;
        public Door door;
        public Elevator(int _id, string _status, int _amountOfFloors, int _currentFloor)
        {
            this.ID = _id;
            this.status = _status;
            this.amountOfFloors = _amountOfFloors;
            this.currentFloor = _currentFloor;
            this.door = new Door(_id, "close");
            this.floorRequestsList = new List<int>();
            this.completedRequestsList = new List<int>();
            this.direction = "none";
        }

        //Make elevator move to desire direction
        public void move()
        {   
            while(this.floorRequestsList.Count != 0)
            {
                int destination = floorRequestsList[0];
                this.status = "moving";
                if(this.direction == "up")
                {
                    while(this.currentFloor < destination)
                    {
                        this.currentFloor++;
                    }
                }
                else if(this.direction == "down")
                {
                    while(this.currentFloor > destination)
                    {
                        this.currentFloor--;
                    }
                }
                this.status = "stopped";
                this.operateDoors();
                this.floorRequestsList.RemoveAt(0);
                this.completedRequestsList.Add(destination);
            }
            this.status = "idle";
        }

        //sort floors in numerical or reverse
        public void sortFloorList()
        {
            if(this.direction == "up")
            {
                this.floorRequestsList.Sort();
            }
            else
            {
                this.floorRequestsList.Reverse();
            }
        }

        //close and open door
        public void operateDoors()
        {
            if(door.status == "opened") 
            {
                door.status = "closed";
            } 
            else if (door.status == "closed")
            {
                door.status = "opened";
            }
        }

        //add new floor request to list
        public void addNewRequest(int userPosition)
        {
            if(!this.floorRequestsList.Contains(userPosition))
            {
                this.floorRequestsList.Add(userPosition);
            }

            if(this.currentFloor < userPosition)
            {
                this.direction = "up";
            }

            if(this.currentFloor > userPosition)
            {
                this.direction = "down";
            }
        }
    }
}