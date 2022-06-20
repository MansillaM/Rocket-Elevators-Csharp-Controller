using System;
using System.Collections.Generic;

int columnID = 1;
int floorRequestButtonID = 1;
int floor;

namespace Commercial_Controller
{   //Class Creation
    public class Battery
    {
        //Initilisation of object Battery
        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            this.ID = _id;
            this.status = "online";
            List<int> columnsList = new List<int>();
            List<int> floorRequestsButtonsList = new List<int>();

            if (_amountOfBasements > 0)
            {
                this.createBasementFloorRequestButtons(int _amountOfBasements);
                this.createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn);
                _amountOfColumns--;
            }
            
            this.createBasementFloorRequestButtons(int _amountOfBasements);
            this.createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfElevatorPerColumn);
        }
        //-----METHODS-----//
        //Basement floors
        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            List<int> servedFloors = new List<int>();
            int floor = -1;
            for (int i = 0 ; i < _amountOfBasements; i++)
            {
                servedFloors.Add(floor);
                floor--;
            }

            Column column = new Column(int columnID, string status, int _amountOfBasements, int _amountOfElevatorPerColumn, int servedFloors, bool true);
            this.columnsList[column];
            columnID++;
        }
        //Amount of columns
        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            int _amountOfElevatorPerColumn = Math.Ceiling(_amountOfFloors/_amountOfColumns);
            int floor = 1;

            for (int i = 0 ; i < _amountOfColumns; i++)
            {
                List<int> servedFloors = new List<int>();
                for(int i = 0 ; i < _amountOfElevatorPerColumn; i++)
                {
                    if(floor >= _amountOfFloors)
                    {
                        servedFloors.Add(floor);
                        floor++;
                    }
                }
                Column column = new Column(int columnID, string status, int _amountOfFloors, int _amountOfElevatorPerColumn, int servedFloors, bool false);
                this.columnsList.Add(column)          
                columnID++;  
            }
        }
        //Button list for floor exept basement
        public void createFloorRequestButtons(int _amountOfFloors)
        {
            int buttonFloor = 1;
            for(int i = 0; i < _amountOfFloors; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(int floorRequestButtonID, string "OFF", int buttonFloor, string "UP");
                this.floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor++;
                floorRequestButtonID++;
            }
        }
        //Button list for basement
        public void createBasementFloorRequestButtons(int _amountOfBasements)
        {
            buttonFloor = -1;
            for(int i = 0; i <_amountOfBasements; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(int floorRequestButtonID, string "OFF", int buttonFloor, string "DOWN");
                this.floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor--;
                floorRequestButtonID++;
            }
        }
        //Find best column for elevator
        public Column findBestColumn(int _requestedFloor)
        {
            foreach(Column column in this.columnsList)
            {
                if(column[servedFloorsList].Contains(_requestedFloor))
                {
                    return column
                }
            }
        }
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            Column column = this.findBestColumn(int _requestedFloor);
            Elevator elevator = column.findElevator(int 1, string "_direction");
            elevator.addNewRequest(int 1);
            elevator.move();

            elevator.addNewRequest(_requestedFloor);
            elevator.move();
        }
    }
}

