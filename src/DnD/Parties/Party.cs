namespace DnD.Parties
{
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