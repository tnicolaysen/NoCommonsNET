using System;
using System.Collections.Generic;
using NoCommons.Common;

namespace NoCommons.Mail
{
public class MailValidator : StringNumberValidator {

	private const int POSTNUMMER_LENGTH = 4;
    private static Dictionary<Poststed, List<Postnummer>> poststedMap = new Dictionary<Poststed, List<Postnummer>>();
    private static Dictionary<Postnummer, Poststed> postnummerMap = new Dictionary<Postnummer, Poststed>();

	public static void setPostnummerMap(Dictionary<Postnummer, Poststed> aPostnummerMap) {
		postnummerMap = aPostnummerMap;
	}

	public static void setPoststedMap(Dictionary<Poststed, List<Postnummer>> aPoststedMap) {
		poststedMap = new Dictionary<Poststed, List<Postnummer>>(aPoststedMap);
	}

	public static int getAntallPoststed() {
      return poststedMap.Count;
	}

	public static int getAntallPostnummer() {
      return postnummerMap.Count;
	}

	public static bool isValidPostnummer(string postnummer) {
		try {
			getPostnummer(postnummer);
			return true;
		} catch (ArgumentException e) {
			return false;
		}
	}

	public static Postnummer getPostnummer(string postnummer) {
		validateSyntax(postnummer);
		return new Postnummer(postnummer);
	}

	public static List<Postnummer> getPostnummerForPoststed(string poststed) {
		var p = new Poststed(poststed);
		List<Postnummer> postnummerList;
        var found = poststedMap.TryGetValue(p, out postnummerList);
        return (found ? postnummerList :  new List<Postnummer>());
	}

	private static void validateSyntax(string postnummer) {
		ValidateLengthAndAllDigits(postnummer, POSTNUMMER_LENGTH);
	}

	public static Poststed getPoststedForPostnummer(string postnummer) {
		Poststed result = null;
		var pn = getPostnummer(postnummer);
		if (postnummerMap.ContainsKey(pn)) {
			result = postnummerMap[pn];
		}
		return result;
	}

}

}
