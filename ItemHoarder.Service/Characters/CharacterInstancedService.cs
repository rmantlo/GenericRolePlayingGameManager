using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class CharacterInstancedService
    {
        private readonly Guid _userId;
        public CharacterInstancedService(Guid userId)
        {
            _userId = userId;
        }
        //get all character instance information
        //get character info by id
        //create new character instance
        //update character instance
            //Cant change class, race, or background
            //can change alignment, attacksAndSpells, characterNotes, money, strength etc,
            //hit points, carry weight determined by system IF DND game, otherwise it is manual
    }
}
