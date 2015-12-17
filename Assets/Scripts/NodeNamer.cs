using UnityEngine;
using System.Collections;

public static class NodeNamer
{
	private static string[] peopleNames = new string[]
	{
		"Amelia","Olivia","Isla","Emily","Poppy","Ava",
		"Isabella","Jessica","Lily","Sophie","Grace",
		"Sophia","Mia","Evie","Ruby","Ella","Scarlett",
		"Isabelle","Chloe","Sienna","Freya","Phoebe",
		"Charlotte","Daisy","Alice","Florence","Eva",
		"Sofia","Millie","Lucy","Evelyn","Elsie","Rosie",
		"Imogen","Lola","Matilda","Elizabeth","Layla",
		"Holly","Lilly","Molly","Erin","Ellie","Maisie",
		"Maya","Abigail","Eliza","Georgia","Jasmine",
		"Esme","Oliver","Jack","Harry","Jacob","Charlie",
		"Thom","George","Oscar","Jim","William","Noah",
		"Alfie","Joshua","Moe","Henry","Leo","Archie",
		"Ethan","Joseph","Freddie","Samuel","Alexander",
		"Logan","Daniel","Isaac","Max","Benjamin",
		"Mason","Luca","Edward","Harrison","Jake",
		"Dylan","Riley","Finley","Theo","Sebastian",
		"Adam","Zachary","Arthur","Toby","Jayden","Luke",
		"Harley","Lee","Tyler","Harvey","Matthew","David",
		"Reuben","Michael"
	};

	private static string[] phoneNames = new string[]
	{
		"Smartphone", "oPhone", "Phone"
	};

	private static string[] desktopNames = new string[]
	{
		"Desktop", "Laptop", "Computer", "PC"
	};

	private static string[] hiEndNames = new string[]
	{
		"Computer", "PC", "Rig", "Machine"
	};

	private static string[] fileServerNames = new string[]
	{
		"Clan Server", "Game Server"
	};

	private static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public static string GetName(MachineType type)
	{
		switch (type)
		{
			case MachineType.SMARTPHONE:
				return peopleNames[Random.Range(0, peopleNames.Length)] + "'s " + phoneNames[Random.Range(0, phoneNames.Length)];
			case MachineType.DESKTOPPC:
				return (Random.value >= 0.5f) ?
					peopleNames[Random.Range(0, peopleNames.Length)] + "'s " + desktopNames[Random.Range(0, desktopNames.Length)]
					: "Terminal " + Random.Range(1, 100);
			case MachineType.HIGHENDPC:
				return (Random.value >= 0.5f) ?
					peopleNames[Random.Range(0, peopleNames.Length)] + "'s " + hiEndNames[Random.Range(0, hiEndNames.Length)]
					: "Workstation " + Random.Range(1, 100);
			case MachineType.FILESERVER:
				return (Random.value >= 0.5f) ?
					"File Server " + Random.Range(1, 100) + ":" + Random.Range(1, 1000)
					: ((Random.value >= 0.5f) ?
					peopleNames[Random.Range(0, peopleNames.Length)] + "'s " + fileServerNames[Random.Range(0, fileServerNames.Length)]
                    : "Research Server " + Random.Range(1, 100) + ":" + Random.Range(1, 1000)
					);
			case MachineType.MININGARRAY:
				return "Array " + alphabet[Random.Range(0, alphabet.Length)] + Random.Range(100,1000);
			case MachineType.SUPERCOMPUTER:
				return alphabet[Random.Range(0, alphabet.Length)].ToString() + 
					alphabet[Random.Range(0, alphabet.Length)] + Random.Range(100, 1000);
			default:
				return "";
		}
	}
}

