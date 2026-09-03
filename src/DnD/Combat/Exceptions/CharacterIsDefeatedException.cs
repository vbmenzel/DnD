namespace DnD.Combat.Exceptions;

/// <summary>
/// The exception thrown when an action cannot be performed because the character is already defeated.
/// </summary>
/// <param name="message">A message describing why the action could not be performed.</param>
public class CharacterIsDefeatedException(string message) : Exception(message);