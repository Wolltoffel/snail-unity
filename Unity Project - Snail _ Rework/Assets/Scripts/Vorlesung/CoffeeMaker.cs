using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Coffee {
    public CoffeeType type;
    public int taste;
    public int quantity;
}

public enum CoffeeType
{
    latte, cappuccino
}
public interface ICoffeeMaker
{
    Coffee Prepare(CoffeeType type, int quantity);
}

public class LowEndCoffeeMaker : ICoffeeMaker
{
    public Coffee Prepare(CoffeeType type, int quantity)
    {
        quantity = Mathf.Clamp(quantity,0, 250);
        return new Coffee() {type =type,taste = 5,quantity = quantity };
    }
}

public class HighEndCoffeeMaker : ICoffeeMaker
{
    public Coffee Prepare(CoffeeType type, int quantity)
    {
        quantity = Mathf.Clamp(quantity, 0,500);
        return new Coffee() { type = type, taste = 10, quantity = quantity };
    }
}

public class UseCoffee: MonoBehaviour
{
    ICoffeeMaker coffeeMaker;
    private void Start()
    {
        //created a random coffee maker
        int rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            coffeeMaker = new LowEndCoffeeMaker() ;
        }
        else
        {
           coffeeMaker = new HighEndCoffeeMaker() ;  
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Make Coffee
            Coffee coffee = coffeeMaker.Prepare(CoffeeType.cappuccino, 400);
            Debug.Log($"Cappucino {coffee.taste}/10 {coffee.quantity} ml");

            if (coffeeMaker is HighEndCoffeeMaker)
            {
                HighEndCoffeeMaker hcm = (HighEndCoffeeMaker) coffeeMaker;
              
            }
        }
    }
}
