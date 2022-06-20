using System;
using System.Collections.Generic;

int elevatorID = 1;
int callButtonID = 1;
int floor;

namespace Commercial_Controller
{
    public class Column
    {
        //Initialize Columns
        public Column(string _ID, string _status, int _amountOfElevators, int _amountOfFloors, List<int> _servedFloors, bool _isBasement)
        {
            string ID = "_id";
            string status = _status;
            int amountOfFloors = _amountOfFloors;
            int amountOfElevators = _amountOfElevators;
            List<int> elevatorsList = new List<int>();
            List<int> callButtonList = new List<int>();
            int servedFloorList = _servedFloors;

            this.createElevators(int _amountOfFloors, int _amountOfElevators);
            this.createCallButtons(int _amountOfFloors, bool _isBasement);
        }
        //Create call buttons based off amount of floors
        public createCallButtons(int _amountOfFloors, bool _isBasement)
        {
            if(_isBasement = true)
                int buttonFloor = -1;
                for(int i = 0; i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(int callButtonID, string "OFF",int buttonFloor, string "Up");
                    this.callButtonList.Add(callButton);
                    buttonFloor--;
                    callButtonID++;
                }
            else
            {
                int buttonFloor = 1;
                for(int i = 0: i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(int callButtonID, string "OFF",int buttonFloor, string "Down");
                    this.callButtonList.Add(callButton); 
                    buttonFloor++;
                    callButtonID++;       
                }
            }
        }
        //Create elevators
        public createElevators(int _amountOfFloors, int _amountOfElevators)
        {
            for(int i = 0; i < _amountOfElevators; i++)
            {
                Elevator elevator = new Elevator(int elevatorID, string "idle", int _amountOfFloors, int 1);
                this.elevatorsList.Add(elevator);
                elevatorID++;
            }
        }

        //Simulate when a user press a button on a floor to go back to the first floor
        public Elevator requestElevator(int userPosition, string direction)
        {
            Elevator elevator = this.findElevator(int userPosition, string direction)
            elevator.addNewRequest(int _requestedFloor);
            elevator.move();

            elevator.addNewRequest(int 1);//Always 1 because the user can only go back to the lobby
            elevator.move();
        }

        //We use a score system depending on the current elevators state. Since the bestScore and the referenceGap are 
        //higher values than what could be possibly calculated, the first elevator will always become the default bestElevator, 
        //before being compared with to other elevators. If two elevators get the same score, the nearest one is prioritized. Unlike
        //the classic algorithm, the logic isn't exactly the same depending on if the request is done in the lobby or on a floor.     
        public bestElevator findElevator(int _requestedFloor, int requestedDirection)
        {
            int bestElevator;
            int bestScore = 6;
            int referenceGap = 10000000;
            int bestElevatorInformations;

            if(_requestedFloor = 1)
            {
                foreach(Elevator elevator in this.elevatorsList)
                {
                    if(1 == elevator.currentFloor && elevator.status == "stopped")
                    {   
                         bestElevatorInformations = this.checkIfElevatorIsBetter(int 1, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else if(1 == elevator.currentFloor && elevator.status == "stopped")
                    {    
                        bestElevatorInformations = this.checkIfElevatorIsBetter(int 2, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else if(1 > elevator.currentFloor && elevator.direction == "up")
                    {   
                         bestElevatorInformations = this.checkIfElevatorIsBetter(int 3, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else if(1 < elevator.currentFloor && elevator.direction == "down")
                    {   
                         bestElevatorInformations = this.checkIfElevatorIsBetter(int 3, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else if(elevator.status == "idle")
                    {   
                        bestElevatorInformations = this.checkIfElevatorIsBetter(int 4, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(int 5, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);

                    }
                    bestElevator = bestElevatorInformations[bestElevator];
                    bestScore = bestElevatorInformations[bestScore];
                    referenceGap = bestElevatorInformations[referenceGap];
                }
            }
            else
            {
                foreach(Elevator elevator in this.elevatorsList)
                {
                    if(_requestedFloor == elevator.currentFloor && elevator.status == "stopped" && requestedDirection == elevator.direction)                   
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(int 1, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else if(_requestedFloor > elevator.currentFloor && elevator.direction == "up" && requestedDirection == "up")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(int 2, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else if(_requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == "down")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(int 2, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else if(elevator.status == "idle")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(int 4, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    else
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(int 5, int elevator, int bestScore, int referenceGap, int bestElevator, int _requestedFloor);
                    }
                    bestElevator = bestElevatorInformations[bestElevator];
                    bestScore = bestElevatorInformations[bestScore];
                    referenceGap = bestElevatorInformations[referenceGap];
               }
            }
            return bestElevator
        }

        //Check which is best option
        public bestElevatorInformations checkIfElevatorIsBetter(int scoreToCheck, int newElevator, int bestScore, int referenceGap, int bestElevator, int floor)
        {
            if(scoreToCheck < besScore)
            {
                bestScore = scoreToCheck;
                bestElevator = newElevator;
                referenceGap = Math.Abs(newElevator.currentFloor - floor);
            }
            else if(bestScore == scoreToCheck)
            {
                gap = Math.Abs(newElevator.currentFloor - floor);
                if(referenceGap > gap)
                {
                    bestElevator = newElevator;
                    referenceGap = gap:
                }
            }
            return bestElevatorInformations
        }
    }

}