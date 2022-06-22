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

        //Make elevator move
        // Note: The problem lies in this function, try translating the following pseudo code instead
        /*
            SEQUENCE move
                WHILE THIS floorRequestsList IS NOT empty
                    SET THIS status TO moving
                    CALL THIS sortFloorList
                    SET destination TO first element of THIS floorRequestsList

                    IF THIS direction EQUALS up
                        WHILE currentFloor IS LESS THAN destination
                            INCREMENT THIS currentFloor
                        ENDWHILE
                    ELSE IF THIS direction EQUALS down
                        WHILE currentFloor IS GREATER THAN destination
                            DECREMENT THIS currentFloor
                        ENDWHILE
                    ENDIF

                    SET THIS status TO stopped
                    CALL THIS operateDoors
                    REMOVE first element of THIS floorRequestsList
                    ADD destination TO THIS completedRequestsList
                ENDWHILE

                SET THIS STATUS to idle
                SET THIS direction to empty
            ENDSEQUENCE

        */
        public void move()
        {   
            while(this.floorRequestsList.Count != 0)
            {
                int destination = floorRequestsList[0];
                this.status = "moving";
                if(this.currentFloor < destination)
                {
                    this.direction = "up";
                    this.sortFloorList();
                    while(this.currentFloor < destination)
                    {
                        this.currentFloor++;
                    }
                }
                else if(this.currentFloor > destination)
                {
                    this.direction = "down";
                    this.sortFloorList();
                    while(this.currentFloor < destination)
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