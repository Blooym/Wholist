using System;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.SubKinds;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Sirensong.Extensions;
using Wholist.Common;
using Wholist.Game;

namespace Wholist.DataStructures
{
    /// <summary>
    ///     Represents a pre-formatted version of a <see cref="PlayerCharacter" /> with slimmed down information.
    /// </summary>
    internal unsafe struct PlayerInfoSlim(IPlayerCharacter basePlayer)
    {
        private readonly IPlayerCharacter character = basePlayer;
        private string name = string.Empty;
        private string companyTag = string.Empty;

        /// <summary>
        ///     The name of the player.
        /// </summary>
        internal string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.name))
                {
                    this.name = this.character.Name.TextValue;
                }
                return this.name;
            }
        }

        /// <summary>
        ///     The colour of the player's name.
        /// </summary>
        internal readonly Vector4 NameColour => PlayerManager.GetPlayerNameColour(this);

        /// <summary>
        ///     The job information of the player.
        /// </summary>
        internal readonly JobInfoSlim Job => new(this.character.ClassJob.RowId);

        /// <summary>
        ///     The company tag of the player.
        /// </summary>
        internal string CompanyTag
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.companyTag))
                {
                    this.companyTag = this.character.ToCsPlayerCharacter()->FreeCompanyTagString;
                }
                return this.companyTag;
            }
        }

        /// <summary>
        ///     The HomeWorld of the player.
        /// </summary>
        internal readonly string HomeWorld
        {
            get
            {
                if (Services.WorldNames.TryGetValue(this.character.HomeWorld.RowId, out var homeName))
                {
                    return homeName;
                }
                homeName = Services.WorldSheet.GetRow(this.character.HomeWorld.RowId)!.Name.ToString().ToTitleCase();
                Services.WorldNames.TryAdd(this.character.HomeWorld.RowId, homeName);
                return homeName;
            }
        }

        /// <summary>
        ///     The distance of the player from the local player.
        /// </summary>
        internal readonly double Distance
        {
            get
            {
                var localPlayer = Services.ClientState.LocalPlayer;
                if (localPlayer == null)
                {
                    return 0;
                }
                return Math.Round((localPlayer.ToCsGameObject()->Position - this.Position).Magnitude, MidpointRounding.ToEven);
            }
        }

        /// <summary>
        ///     Whether or not the player is a friend of the local player.
        /// </summary>
        internal readonly bool IsFriend => this.character.ToCsPlayerCharacter()->IsFriend;

        /// <summary>
        ///     Whether or not the player is in the local player's party.
        /// </summary>
        internal readonly bool IsInParty => this.character.ToCsPlayerCharacter()->IsPartyMember;

        /// <summary>
        ///     Whether or not the player is known to the local player (i.e. in party or friend).
        /// </summary>
        internal readonly bool IsKnownPlayer => this.IsInParty || this.IsFriend;

        /// <summary>
        ///     The level of the player.
        /// </summary>
        internal readonly byte Level => this.character.Level;

        /// <summary>
        ///     The pointer for the character data.
        /// </summary>
        internal readonly nint CharacterPtr => this.character.Address;

        /// <summary>
        ///     The location of the player.
        /// </summary>
        internal readonly FFXIVClientStructs.FFXIV.Common.Math.Vector3 Position => this.character.Position;

        /// <summary>
        ///     Targets the player.
        /// </summary>
        internal readonly void Target() => TargetSystem.Instance()->Target = this.character.ToCsGameObject();

        /// <summary>
        ///     Focus targets the player.
        /// </summary>
        internal readonly void FocusTarget() => TargetSystem.Instance()->FocusTarget = this.character.ToCsGameObject();

        /// <summary>
        ///     Opens the examine window for the player.
        /// </summary>
        internal readonly void OpenExamine() => AgentInspect.Instance()->ExamineCharacter(this.character.EntityId);

        /// <summary>
        ///     Opens the character card for the player.
        /// </summary>
        internal readonly void OpenCharaCard() => AgentCharaCard.Instance()->OpenCharaCard(this.character.ToCsGameObject());
    }
}
