using System;
using System.Collections.Generic;

namespace Commercial_Controller
{   //Class Creation
    public class Battery
    {
        public int ID;
        public int amountOfColumns;
        public string status;
        public int amountOfFloors;
        public int amountOfBasements;
        public List<Column> columnsList;
        public List<FloorRequestButton> floorButtonsList;
        public List<int> servedFloors;
        //Initilisation of object Battery
        public Battery(int _id, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            this.ID = _id;
            this.status = "online";
            this.amountOfColumns = _amountOfColumns;
            this.amountOfFloors = _amountOfFloors;
            this.amountOfBasements = _amountOfBasements;
            this.columnsList = new List<Column>();
            this.floorButtonsList = new List<FloorRequestButton>();

            if (_amountOfBasements > 0)
            {
                this.createBasementFloorRequestButtons(_amountOfBasements);
                this.createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
                _amountOfColumns--;
            }
            
            createBasementFloorRequestButtons(_amountOfBasements);
            createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);
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

            Column column = new Column(Global.columnID, status, _amountOfBasements, _amountOfElevatorPerColumn, servedFloors, true);
            columnsList.Add(column);
            Global.columnID++;
        }
        //Amount of columns
        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfElevatorPerColumn)
        {
            _amountOfElevatorPerColumn = (int)Math.Ceiling((double)_amountOfFloors/_amountOfColumns);
            int floor = 1;

            for (int i = 0 ; i < _amountOfColumns; i++)
            {
                List<int> servedFloors = new List<int>();
                for(int m = 0 ; m < _amountOfElevatorPerColumn; m++)
                {
                    if(floor >= _amountOfFloors)
                    {
                        servedFloors.Add(floor);
                        floor++;
                    }
                }
                Column column = new Column(Global.columnID, status, _amountOfFloors, _amountOfElevatorPerColumn, servedFloors, false);
                columnsList.Add(column);    
                Global.columnID++;  
            }
        }
        //Button list for floor exept basement
        public void createFloorRequestButtons(int _amountOfFloors)
        {
            int buttonFloor = 1;
            for(int i = 0; i < _amountOfFloors; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(Global.floorRequestButtonID, "OFF", buttonFloor, "UP");
                this.floorButtonsList.Add(floorRequestButton);
                buttonFloor++;
                Global.floorRequestButtonID++;
            }
        }
        //Button list for basement
        public void createBasementFloorRequestButtons(int _amountOfBasements)
        {
            int buttonFloor = -1;
            for(int i = 0; i <_amountOfBasements; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(Global.floorRequestButtonID, "OFF", buttonFloor, "DOWN");
                this.floorButtonsList.Add(floorRequestButton);
                buttonFloor--;
                Global.floorRequestButtonID++;
            }
        }
        //Find best column for elevator
        public Column findBestColumn(int _requestedFloor)
        {
            foreach(Column column in this.columnsList)
            {
                if(column.servedFloorsList.Contains(_requestedFloor))
                {
                    return column;
                }
            }
            return null;
        }
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            Column column = this.findBestColumn(_requestedFloor);
            Elevator elevator = column.findElevator(1, _direction);
            elevator.addNewRequest(1);
            elevator.move();

            elevator.addNewRequest(_requestedFloor);
            elevator.move();
            return (column, elevator);
        }
    }
}

