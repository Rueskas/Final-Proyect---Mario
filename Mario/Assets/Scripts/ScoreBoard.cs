//Sergio Ruescas
using System;
using System.Collections.Generic;
using System.IO;

class ScoreBoard
{
    private Dictionary<string, int> scores;
    private List<int> punctuations;
    private string path;

    public ScoreBoard(string path)
    {
        punctuations = new List<int>();
        scores = new Dictionary<string, int>();
        this.path = path;
        Load(path);
    }

    public Dictionary<string, int> GetScores()
    {
        return scores;
    }

    public List<string> GetNamesSorted()
    {
        punctuations.Sort();
        List<string> namesSorted = new List<string>();
        for (int i = punctuations.Count - 1; i >= 0; i--)
        {
            foreach (KeyValuePair<string, int> keyValuePair in scores)
            {
                if (keyValuePair.Value == punctuations[i] &&
                        !namesSorted.Contains(keyValuePair.Key))
                    namesSorted.Add(keyValuePair.Key);
            }
        }
        return namesSorted;
    }

    public int GetScore(string name)
    {
        return scores[name];
    }

    public bool Add(string name, int punctuation)
    {
        if (!scores.ContainsKey(name))
        {
            if (punctuations.Count < 5)
            {
                scores.Add(name, punctuation);
                punctuations.Add(punctuation);
            }
            else
            {
                punctuations.Sort();
                string keyToRemove = "";
                for (int i = 0; i < punctuations.Count; i++)
                {
                    if (punctuation > punctuations[i] && keyToRemove == "")
                    {
                        foreach (KeyValuePair<string, int> keyValuePair 
                                                                in scores)
                        {
                            if (keyValuePair.Value == punctuations[i] &&
                                keyToRemove == "")
                            {
                                keyToRemove = keyValuePair.Key;
                            }
                        }
                        scores.Remove(keyToRemove);
                        scores.Add(name, punctuation);

                        punctuations.RemoveAt(i);
                        punctuations.Insert(i, punctuation);
                    }
                }
            }
            return true;
        }
        else //The name exists
            return false;
    }

    public bool Save()
    {
        try
        {
            StreamWriter scoreWriter = new StreamWriter(path);
            foreach (KeyValuePair<string, int> keyValuePair in scores)
            {
                scoreWriter.WriteLine(keyValuePair.Key + ";" + keyValuePair.Value);
            }
            scoreWriter.Close();
            return true;
        }
        catch (PathTooLongException)
        {
            return false;
        }
        catch (IOException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Load(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                string nameToAdd;
                int punctuationToAdd;
                StreamReader scoreReader = new StreamReader(path);
                string linea;
                do
                {
                    linea = scoreReader.ReadLine();
                    if (linea != null)
                    {
                        nameToAdd = linea.Substring(0, linea.IndexOf(';'));
                        punctuationToAdd = Convert.ToInt32(
                            linea.Substring(linea.IndexOf(';') + 1));
                        scores.Add(nameToAdd, punctuationToAdd);
                        punctuations.Add(punctuationToAdd);
                    }
                } while (linea != null);
                scoreReader.Close();
                return true;
            }
            catch (PathTooLongException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return false;
    }
}
