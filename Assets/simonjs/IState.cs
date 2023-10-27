using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IState
{
  
    public  void StateUpdate();
    public  void EnterState();
    public  void ExitState();
}
