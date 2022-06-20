using System.Threading;
using System.Collections.Generic;

int floor;

namespace Commercial_Controller
{
    public class Elevator
    {
        public Elevator(string _id, string _status, int _amountOfFloors, int _currentFloor)
        {
            int ID = _id;
            string status = _status;
            int amountOfFloors = _amountOfFloors;
            int currentFloor = _currentFloor;
            Door Door = new Door(int _id, string "closed");
            List<int> floorRequestList = new List<int>();
            Nullable<int> direction = null;
            bool overweight = false;
        }
        
        //Make elevator move
        public void move()
        {
            while(this.floorRequestList != 0)
            {
                int destination = List<int>.Insert(0, destination);
                string this.status = "moving";
                if(this.currentFloor < destination)
                {
                    string this.direction = "up";
                    this.sortFlootList;
                    while(this.currentFloor < destination)
                    {
                        this.currentFloor++;
                        this.screenDisplay = this.currentFloor;
                    }
                }
                else if(this.currentFloor > destination)
                {
                    string this.direction = "down":
                    this.sortFlootList;
                    while(this.currentFloor < destination)
                    {
                        this.currentFloor--;
                        this.screenDisplay = this.currentFloor;
                    }
                }
                string this.status = "stopped":
                this.operateDoors;
                this.floorRequestList.RemoveAt(0);
            }
            string this.status = "idle";
        }

        //close and open door
        public void operateDoors()
        {
            if(this.door.status == 'opened') 
            {
                this.door.status = 'closed';
            } 
            else if (this.door.status == 'closed')
            {
                this.door.status = 'opened';
            }
        }

        public void addNewRequest(int _requestedFloor)
        {
            if(!this.floorRequestList.Contain(_requestedFloor))
            {
                this.floorRequestList.Add(_requestedFloor);
            }

            if(this.currentFloor < _requestedFloor)
            {
                string this.direction = "up";
            }

            if(this.currentFloor > _requestedFloor)
            {
                string this.direction = "down";
            }
        }
    }
}