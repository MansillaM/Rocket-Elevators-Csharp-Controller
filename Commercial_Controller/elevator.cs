using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Elevator
    {
        public int ID;
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
        
        //Make elevator move
        public void move()
        {
            while(this.floorRequestsList.Count == 0)
            {
                int destination = floorRequestsList[0];
                this.status = "moving";
                if(this.currentFloor < destination)
                {
                    direction = "up";
                    //this.sortFlootList();
                    while(this.currentFloor < destination)
                    {
                        this.currentFloor++;
                       // this.screenDisplay = this.currentFloor;
                    }
                }
                else if(this.currentFloor > destination)
                {
                    direction = "down";
                    //this.sortFlootList;
                    while(this.currentFloor < destination)
                    {
                        this.currentFloor--;
                        //this.screenDisplay = this.currentFloor;
                    }
                }
                this.status = "stopped";
                this.completedRequestsList.Add(this.floorRequestsList[0]);
                this.operateDoors();
                this.floorRequestsList.RemoveAt(0);
            }
            this.status = "idle";
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

        public void addNewRequest(int _requestedFloor)
        {
            if(!this.floorRequestsList.Contains(_requestedFloor))
            {
                this.floorRequestsList.Add(_requestedFloor);
            }

            if(this.currentFloor < _requestedFloor)
            {
                this.direction = "up";
            }

            if(this.currentFloor > _requestedFloor)
            {
                this.direction = "down";
            }
        }
    }
}