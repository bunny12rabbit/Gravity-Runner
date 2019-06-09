using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MyMethods<T> where T : Item
{
    /// <summary>
    /// Returns random item from collection with a chance from position
    /// </summary>
    /// <param name="items">List of items, inherited from class Item</param>
    /// <param name="transform">Transform evaluate with</param>
    /// <returns>Item</returns>
    public static T GetRandomItem(List<T> items, Transform transform)
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < items.Count; i++)
        {
            chances.Add(items[i].ChanceFromDistance.Evaluate(transform.position.x));
        }

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return items[i];
            }
        }

        return items[items.Count - 1];
    }
}
