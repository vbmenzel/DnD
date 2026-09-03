namespace DnD.Parties
{
	// Party holder styr på gruppens Characters.
	// Character kommer fra Person 1 og skal tilpasses,
	// hvis hans namespace eller klassenavn ændres.
	public class Party
	{
		private readonly List<Character> members = new();

		public void AddMember(Character character)
		{
			members.Add(character);
		}

		public void RemoveMember(Character character)
		{
			members.Remove(character);
		}

		public IReadOnlyList<Character> GetMembers()
		{
			return members.AsReadOnly();
		}
	}
}