#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System;

class RXSolutionFixer : AssetPostprocessor 
{
	private static void OnGeneratedCSProjectFiles() //secret method called by unity after it generates the solution
	{
		string currentDir = Directory.GetCurrentDirectory();
		string[] csprojFiles = Directory.GetFiles(currentDir, "*.csproj");
		
		foreach(var filePath in csprojFiles)
		{
			FixProject(filePath);
		}
	}
	
	static bool FixProject(string filePath)
	{
		string content = File.ReadAllText(filePath);

		string searchString1 = "<TargetFrameworkVersion>v3.5</TargetFrameworkVersion>";
		string replaceString1 = "<TargetFrameworkVersion>v4.5</TargetFrameworkVersion>";
		string searchString2 = "<LangVersion>4</LangVersion>";
		string replaceString2 = "<LangVersion>4</LangVersion>\n    <UseMSBuildEngine>False</UseMSBuildEngine>";

		if(content.IndexOf(searchString1) != -1 && content.IndexOf(searchString2) != -1)
		{
			content = Regex.Replace(content,searchString1,replaceString1);
			content = Regex.Replace(content,searchString2,replaceString2);
			File.WriteAllText(filePath,content);
			return true;
		}
		else 
		{
			return false;
		}
	}

}
#endif