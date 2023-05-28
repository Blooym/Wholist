using System.Numerics;
using Dalamud.Game.ClientState.Objects.SubKinds;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Sirensong.Game.Extensions;
using Wholist.Common;
using Wholist.Game;

namespace Wholist.DataStructures
{
    /// <summary>
    ///     Represents a pre-formatted version of a <see cref="PlayerCharacter" /> with slimmed down information.
    /// </summary>
    internal readonly unsafe struct PlayerInfoSlim
    {
        /// <summary>
        ///     The underlying <see cref="playerCharacter" /> to pull information from.
        /// </summary>
        private readonly PlayerCharacter playerCharacter;

        /// <summary>
        ///     Creates a new <see cref="PlayerInfoSlim" />.
        /// </summary>
        /// <param name="basePlayer">The base player to create the <see cref="PlayerInfoSlim" /> from.</param>
        internal PlayerInfoSlim(PlayerCharacter basePlayer)
        {
            this.playerCharacter = basePlayer;

            this.Homeworld = new WorldInfoSlim(basePlayer.HomeWorld.GameData!);
            this.Job = new JobInfoSlim(basePlayer.ClassJob.GameData!);

            this.Name = basePlayer.Name.TextValue;
            this.CompanyTag = basePlayer.CompanyTag.TextValue;
            this.IsFriend = basePlayer.IsFriend();
            this.IsInParty = PlayerManager.IsPlayerInParty(basePlayer);
            this.NameColour = PlayerManager.GetPlayerNameColour(this);
        }

        /// <summary>
        ///     The name of the player.
        /// </summary>
        internal readonly string Name;

        /// <summary>
        ///     The colour of the player's name.
        /// </summary>
        internal readonly Vector4 NameColour;

        /// <summary>
        ///     The job information of the player.
        /// </summary>
        internal readonly JobInfoSlim Job;

        /// <summary>
        ///     The company tag of the player.
        /// </summary>
        internal readonly string CompanyTag;

        /// <summary>
        ///     The homeworld of the player.
        /// </summary>
        internal readonly WorldInfoSlim Homeworld;

        /// <summary>
        ///     Whether or not the player is a friend of the local player.
        /// </summary>
        internal readonly bool IsFriend;

        /// <summary>
        ///     Whether or not the player is in the local player's party.
        /// </summary>
        internal readonly bool IsInParty;

        /// <summary>
        ///     Whether or not the player is known to the local player (i.e. in party or friend).
        /// </summary>
        internal bool IsKnownPlayer => this.IsInParty || this.IsFriend;

        /// <summary>
        ///     The level of the player.
        /// </summary>
        internal byte Level => this.playerCharacter.Level;

        /// <summary>
        ///     The online status id of the player.
        /// </summary>
        internal uint OnlineStatusId => this.playerCharacter.OnlineStatus.Id;

        /// <summary>
        ///     The location of the player.
        /// </summary>
        internal Vector3? Position => this.playerCharacter.Position;

        /// <summary>
        ///     Gets the underlying <see cref="PlayerCharacter" />.
        /// </summary>
        /// <returns>The underlying <see cref="PlayerCharacter" />.</returns>
        internal PlayerCharacter GetPlayerCharacter() => this.playerCharacter;

        /// <summary>
        ///     Opens the examine window for the player.
        /// </summary>
        internal void OpenExamine() => AgentInspect.Instance()->ExamineCharacter(this.playerCharacter.ObjectId);

        /// <summary>
        ///     Targets the player.
        /// </summary>
        internal void Target() => Services.TargetManager.SetTarget(this.playerCharacter.Address);

        /// <summary>
        ///     Opens the character card for the player.
        /// </summary>
        internal void OpenCharaCard() => AgentCharaCard.Instance()->OpenCharaCard(this.playerCharacter.ToCsGameObject());
    }
}
