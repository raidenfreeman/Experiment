using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Recipe = System.Collections.Generic.Dictionary<ContentType, int>;

public interface ICombinator
{
    bool TryAddIngredient(ContentType ingredient);
}


public class Pot : MonoBehaviour, IPickableItem, ICombinator
{

    /// <summary>
    /// Set in editor, the point where to place the left hand
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    [SerializeField]
    private Transform leftHandAnchor;

    /// <summary>
    /// Set in editor, the point where to place the left hand
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    public Transform LeftHandAnchor
    {
        get
        {
            return leftHandAnchor;
        }
    }

    /// <summary>
    /// Set in editor, the point where to place the right hand
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    [SerializeField]
    private Transform rightHandAnchor;
    /// <summary>
    /// Set in editor, the point where to place the right hand
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    public Transform RightHandAnchor
    {
        get
        {
            return rightHandAnchor;
        }
    }
    /// <summary>
    /// Set in editor, used to position on surfaces
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    [SerializeField]
    private Transform placementAnchor;
    /// <summary>
    /// Set in editor, used to position on surfaces
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    public Transform PlacementAnchor
    {
        get
        {
            return placementAnchor;
        }
    }

    public void Drop()
    {
        throw new NotImplementedException();
    }

    public void PickUp()
    {
        throw new NotImplementedException();
    }

    public void Place(PlacementSurface surface)
    {
        throw new NotImplementedException();
    }

    public bool TryAddIngredient(ContentType ingredient)
    {

        bool canAddIngredient = recipes
            .Where(x => x.ContainsKey(ingredient))// filter the recipes that contain the ingredient
            .Where(x => x.Keys.Intersect(contents.Keys).Count() == contents.Count) // 
            .Where(x => contents.All(y => x[y.Key] >= y.Value))
            .Any(x =>
            {
                if (contents.ContainsKey(ingredient))
                {
                    return x[ingredient] > contents[ingredient];
                }
                else
                {
                    return true;
                }
            });// is there any recipe, that has this ingredient more times than it already exists in contents
        if (canAddIngredient)
        {
            if (contents.ContainsKey(ingredient))
            {
                contents[ingredient] += 1;
            }
            else
            {
                contents[ingredient] = 1;
            }
            UpdateContentsDisplay();
        }
        foreach (var item in contents)
        {
            Debug.Log($"{item.Value.ToString()} x {item.Key.ToString()}");
        }
        Debug.Log("================");
        return canAddIngredient;
    }

    Dictionary<ContentType, int> contents = new Dictionary<ContentType, int>();

    public Dictionary<ContentType, int> Contents
    {
        get
        {
            return contents;
        }

        private set
        {
            contents = value;
            UpdateContentsDisplay();
        }
    }

    List<Recipe> recipes = new List<Recipe>
    {
        new Recipe
        {
            { ContentType.tomato, 3 }
        },
        new Recipe
        {
            { ContentType.onion, 3 }
        }
    };

    [SerializeField]
    GameObject Canvas;
    [SerializeField]
    Transform ContentDisplayPanel;
    void UpdateContentsDisplay()
    {
        if (ContentDisplayPanel != null)
        {
            foreach (Transform child in ContentDisplayPanel)
            {
                Destroy(child.gameObject);
            }
            foreach (var item in Contents)
            {
                var image = IngredientToImage(item.Key);
                for (int i = 0; i < item.Value; i++)
                {
                    Instantiate(image, ContentDisplayPanel);
                }
            }
            Canvas.SetActive(Contents.Count > 0);
        }
    }

    [SerializeField]
    GameObject tomatoImage;

    GameObject IngredientToImage(ContentType ingredient)
    {
        return tomatoImage;
    }

}