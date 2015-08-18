using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordList : MonoBehaviour {

	public static WordList S;		//singleton
	public TextAsset wordListText;	//the text file
	public int numToParseBeforeYield = 10000;	//parse 10000 lines at a time
	public int wordLengthMin = 3;	// min word length
	public int wordLengthMax = 7;	//max word length
	public int currLine = 0;		//the index of the current line
	public int totalLines;			//total lines in file
	public int longWordCount;		//count for # of max lenght words
	public int wordCount;			//the word count
	// PRIVATE VARIABLES
	private string[] lines;			//holds the lines as they are read in
	private List<string> longWords;	//a list for the long (max length) words
	private List<string> words;		//a list for the other words


	void Awake()
	{
		S = this;
	}



	public void Init () 
	{
		lines = wordListText.text.Split ('\n');
		totalLines = lines.Length;

		StartCoroutine (ParseLines ());
	}

	public IEnumerator ParseLines(){
		string word;

		longWords = new List<string> ();
		words = new List<string> ();

		for (currLine = 0; currLine < totalLines; currLine++) {
			word = lines[currLine];
			if (word.Length == wordLengthMax){
				longWords.Add(word);
			}
			if (word.Length >= wordLengthMin && word.Length <= wordLengthMax){
				words.Add(word);
			}

			if (currLine % numToParseBeforeYield == 0){

				longWordCount = longWords.Count;
				wordCount = words.Count;

				yield return null;
			}
		}
		gameObject.SendMessage("WordListParseComplete");

	}
	public List<string> GetWords(){
		return (words);
	}
	public string GetWord(int ndx){
		return(words[ndx]);
	}
	public List<string> GetLongWords(){
		return(longWords);
	}
	public string GetLongWord(int ndx){
		return(longWords [ndx]);
	}


}
