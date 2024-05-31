using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class SquareTextureData : ScriptableObject
{
    [System.Serializable]
    public class TextureData
    {
        public Sprite texture;
        public Config.SquareColor squareColor;

    }

    public List<TextureData> activeSquareTextures;
    public Config.SquareColor currentColor;

    public int GetCurrentColorIndex()
    {
        var currentIndex = 0;
        for (int index = 0; index < activeSquareTextures.Count; index++)
        {
            if (activeSquareTextures[index].squareColor == currentColor)
            {
                currentIndex = index;
            }

        }
        return currentIndex;
    }

    public void RandomColor()
    {
        currentColor = activeSquareTextures[UnityEngine.Random.Range(0,7)].squareColor;
    }

	public void GameOverColor()
    {
		currentColor = activeSquareTextures[8].squareColor;
	}
	private void Awake()
	{
		RandomColor();
        
	}

	private void OnEnable()
	{
        RandomColor();
	}
}
