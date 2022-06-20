using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
        public int ID;
        public string status;
        public int amountOfFloors;
        public int amountOfElevators;
        public List<Elevator> elevatorsList;
        public List<CallButton> callButtonsList;
        public List<int> servedFloorsList;
        public int callButtonId = 1;
        //Initialize Columns
        public Column(int _id, string _status, int _amountOfElevators, int _amountOfFloors, List<int> _servedFloors, bool _isBasement)
        {
            this.ID = _id;
            this.status = _status;
            this.amountOfFloors = _amountOfFloors;
            this.amountOfElevators = _amountOfElevators;
            this.servedFloorsList = _servedFloors;
            this.elevatorsList = new List<Elevator>();
            this.callButtonsList = new List<CallButton>();

            createElevators(_amountOfFloors, _amountOfElevators);
            createCallButtons(_amountOfFloors, _isBasement);
        }
        //Create call buttons based off amount of floors
        public void createCallButtons(int _amountOfFloors, bool _isBasement)
        {
            if(_isBasement)
            {
                int buttonFloor = -1;
                for(int i = 0; i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(Global.callButtonID, "OFF", buttonFloor, "Up");
                    this.callButtonsList.Add(callButton);
                    buttonFloor--;
                    Global.callButtonID++;
                }
            }    
            else
            {
                int buttonFloor = 1;
                for(int i = 0; i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(Global.callButtonID, "OFF", buttonFloor, "Down");
                    this.callButtonsList.Add(callButton); 
                    buttonFloor++;
                    Global.callButtonID++;       
                }
            }
        }
        //Create elevators
        public void createElevators(int _amountOfFloors, int _amountOfElevators)
        {
            for(int i = 0; i < _amountOfElevators; i++)
            {
                Elevator elevator = new Elevator(Global.elevatorID, "idle", _amountOfFloors, 1);
                this.elevatorsList.Add(elevator);
                Global.elevatorID++;
            }
        }

        //Simulate when a user press a button on a floor to go back to the first floor
        public Elevator requestElevator(int userPosition, string direction)
        {
            Elevator elevator = this.findElevator(userPosition, direction);
            //elevator.addNewRequest(_requestedFloor);
            //elevator.move();

            elevator.addNewRequest(1);//Always 1 because the user can only go back to the lobby
            elevator.move();
        }

        //We use a score system depending on the current elevators state. Since the bestScore and the referenceGap are 
        //higher values than what could be possibly calculated, the first elevator will always become the default bestElevator, 
        //before being compared with to other elevators. If two elevators get the same score, the nearest one is prioritized. Unlike
        //the classic algorithm, the logic isn't exactly the same depending on if the request is done in the lobby or on a floor.     
        public Elevator findElevator(int _requestedFloor, string requestedDirection)
        {
            BestElevatorInformations bestElevatorInformations = new BestElevatorInformations();
            bestElevatorInformations.bestElevator=  null;
            bestElevatorInformations.bestScore = 6;
            bestElevatorInformations.referenceGap = 10000000;
               

            if(_requestedFloor == 1)
            {
                foreach(Elevator elevator in this.elevatorsList)
                {
                    if(1 == elevator.currentFloor && elevator.status == "stopped")
                    {   
                         bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else if(1 == elevator.currentFloor && elevator.status == "stopped")
                    {    
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else if(1 > elevator.currentFloor && elevator.direction == "up")
                    {   
                         bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else if(1 < elevator.currentFloor && elevator.direction == "down")
                    {   
                         bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else if(elevator.status == "idle")
                    {   
                        bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, _requestedFloor);

                    }
                    //bestElevator = bestElevatorInformations[bestElevator];
                   // bestScore = bestElevatorInformations[bestScore];
                    //referenceGap = bestElevatorInformations[referenceGap];
                }
                    return bestElevatorInformations.bestElevator;
            }
            else
            {
                foreach(Elevator elevator in this.elevatorsList)
                {
                    if(_requestedFloor == elevator.currentFloor && elevator.status == "stopped" && requestedDirection == elevator.direction)                   
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else if(_requestedFloor > elevator.currentFloor && elevator.direction == "up" && requestedDirection == "up")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else if(_requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == "down")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else if(elevator.status == "idle")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, _requestedFloor);
                    }
                    else
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, _requestedFloor);
                    }
               }
                    return bestElevatorInformations.bestElevator;

            }
        }

        //Check which is best option
        public BestElevatorInformations checkIfElevatorIsBetter(int scoreToCheck,Elevator newElevator, BestElevatorInformations bestElevatorInformations, int floor)
        {
            if(scoreToCheck < bestElevatorInformations.bestScore)
            {
                bestElevatorInformations.bestScore = scoreToCheck;
                bestElevatorInformations.bestElevator = newElevator;
                bestElevatorInformations.referenceGap = Math.Abs(newElevator.currentFloor - floor);
            }
            else if(bestElevatorInformations.bestScore == scoreToCheck)
            {
                int gap = Math.Abs(newElevator.currentFloor - floor);
                if(bestElevatorInformations.referenceGap > gap)
                {
                    bestElevatorInformations.bestElevator = newElevator;
                    bestElevatorInformations.referenceGap = gap;
                }
            }
            return bestElevatorInformations;
        }
    }

}