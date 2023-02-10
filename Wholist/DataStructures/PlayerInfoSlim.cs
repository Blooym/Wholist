using System.Numerics;
using Dalamud.Game.ClientState.Objects.SubKinds;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Sirensong.Game.Extensions;
using Wholist.Common;
using Wholist.Game;

namespace Wholist.DataStructures
{
    /// <summary>
    /// Represents a pre-formatted version of a <see cref="PlayerCharacter" /> with slimmed down information.
    /// </summary>
    internal readonly unsafe struct PlayerInfoSlim
    {
        /// <summary>
        /// The underlying <see cref="playerCharacter" /> to pull information from.
        /// </summary>
        private readonly PlayerCharacter playerCharacter;

        /// <summary>
        /// Creates a new <see cref="PlayerInfoSlim" />.
        /// </summary>
        /// <param name="basePlayer">The base player to create the <see cref="PlayerInfoSlim" /> from.</param>
        public PlayerInfoSlim(PlayerCharacter basePlayer)
        {
            this.playerCharacter = basePlayer;

            this.Homeworld = new WorldInfoSlim(basePlayer.HomeWorld.GameData!);
            this.Class = new JobInfoSlim(basePlayer.ClassJob.GameData!);

            this.Name = basePlayer.Name.TextValue;
            this.Level = basePlayer.Level;
            this.CompanyTag = basePlayer.CompanyTag.TextValue;
            this.OnlineStatusId = basePlayer.OnlineStatus.Id;
            this.IsFriend = PlayerManager.IsPlayerOnFriendlist(basePlayer);
            this.IsInParty = PlayerManager.IsPlayerInParty(basePlayer);
            this.NameColour = PlayerManager.GetColourForPlayer(this);
        }

        /// <summary>
        /// The name of the player.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The colour of the player's name.
        /// </summary>
        public readonly Vector4 NameColour;

        /// <summary>
        /// The level of the player.
        /// </summary>
        public readonly byte Level;

        /// <summary>
        /// The class information of the player.
        /// </summary>
        public readonly JobInfoSlim Class;

        /// <summary>
        /// The company tag of the player.
        /// </summary>
        public readonly string CompanyTag;

        /// <summary>
        /// The online status id of the player.
        /// </summary>
        public readonly uint OnlineStatusId;

        /// <summary>
        /// The homeworld of the player.
        /// </summary>
        public readonly WorldInfoSlim Homeworld;

        /// <summary>
        /// Whether or not the player is a friend of the local player.
        /// </summary>
        public readonly bool IsFriend;

        /// <summary>
        /// Whether or not the player is in the local player's party.
        /// </summary>
        public readonly bool IsInParty;

        /// <summary>
        /// The location of the player.
        /// </summary>
        public Vector3? Position => this.playerCharacter.Position;

        /// <summary>
        /// Opens the examine window for the player.
        /// </summary>
        public void OpenExamine() => AgentInspect.Instance()->ExamineCharacter(this.playerCharacter.ObjectId);

        /// <summary>
        /// Targets the player.
        /// </summary>
        public void Target() => Services.TargetManager.SetTarget(this.playerCharacter.Address);

        /// <summary>
        /// Opens the character card for the player.
        /// </summary>
        public void OpenCharaCard() => AgentCharaCard.Instance()->OpenCharaCard(this.playerCharacter.ToCSGameObject());
    }
}