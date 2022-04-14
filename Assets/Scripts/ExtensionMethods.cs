using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Gets child from transform with the specified name.
    /// </summary>
    /// <param name="transform">Target transform.</param>
    /// <param name="name">Child name.</param>
    /// <returns></returns>
    public static Transform GetChildWithName(this Transform transform, string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals(name))
                return child;
        }

        return null;
    }

    /// <summary>
    /// Recursively gets child from transform with the specified name.
    /// </summary>
    /// <param name="transform">Target transform.</param>
    /// <param name="name">Child name.</param>
    /// <returns></returns>
    public static Transform GetChildWithNameRecursive(this Transform transform, string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals(name))
                return child;
            else
            {
                Transform foundTransform = child.GetChildWithNameRecursive(name);
                if (foundTransform != null)
                    return foundTransform;
            }
        }

        return null;
    }
}
