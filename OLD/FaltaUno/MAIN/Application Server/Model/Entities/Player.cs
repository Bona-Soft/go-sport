using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities
{
	public class Player : Entity, IPlayer
	{
		//Plantear una interfaz o una clase padre que sea player y luego una para futbol otra para basquet. O position deberia ser un IPosition generica.
		public key<long> PlayerID { get; private set; }
		public object WeeklyAvailability { get; set; }
		public string Alias { get; set; }
		public bool Active { get; set; }

		public IUser User { get; set; }
		public ISport Sport { get; set; }

		public List<IField> Fields { get; set; }
		public List<IChallengeType> ChallengeTypes { get; set; }
		public List<IFieldPosition> PlayerPositions { get; set; }

		public Player(long playerID)
		{
			PlayerID = playerID;
		}

		public Player()
		{
			PlayerID = 0;
		}


		//TODO: Completar datos de Player, hacer publicas los ejemplos de abajo
		private string UniformName; // Markings; //El nombre para mostrar en la camiseta
		

		private Type HexagonAbilities; // Speed, Attack, Defense, Power, Technique , Stamina, Jump
												 // Velocidad, Ataque, Defensa, Poder, Tecnica, Resistencia, Fair Play/Comportamiento

		private byte InitialPoints;
	}
}