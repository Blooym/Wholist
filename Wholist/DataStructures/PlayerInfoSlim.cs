using System;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Memory;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Sirensong.Extensions;
using Wholist.Common;
using Wholist.Game;

namespace Wholist.DataStructures
{
    /// <summary>
    ///     Represents a pre-formatted version of a <see cref="PlayerCharacter" /> with slimmed down information.
    /// </summary>
    internal unsafe struct PlayerInfoSlim
    {
        private readonly Character* character;
        private string name = string.Empty;
        private string companyTag = string.Empty;

        public PlayerInfoSlim(Character* basePlayer) => this.character = basePlayer;

        /// <summary>
        ///     The name of the player.
        /// </summary>
        internal string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.name))
                {
                    this.name = MemoryHelper.ReadStringNullTerminated((nint)this.character->GameObject.Name);
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
        internal readonly JobInfoSlim Job => new(this.character->CharacterData.ClassJob);

        /// <summary>
        ///     The company tag of the player.
        /// </summary>
        internal string CompanyTag
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.companyTag))
                {
                    this.companyTag = MemoryHelper.ReadStringNullTerminated((nint)this.character->FreeCompanyTag);
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
                if (Services.WorldNames.TryGetValue(this.character->HomeWorld, out var homeName))
                {
                    return homeName;
                }
                homeName = Services.WorldCache.GetRow(this.character->HomeWorld)!.Name.RawString.ToTitleCase();
                Services.WorldNames.TryAdd(this.character->HomeWorld, homeName);
                return homeName;
            }
        }

        /// <summary>
        ///     The distance of the player from the local player.
        /// </summary>
        internal readonly double Distance => Math.Round((Control.Instance()->LocalPlayer->Character.GameObject.Position - this.Position).Magnitude, MidpointRounding.ToEven);

        /// <summary>
        ///     Whether or not the player is a friend of the local player.
        /// </summary>
        internal readonly bool IsFriend => this.character->IsFriend;

        /// <summary>
        ///     Whether or not the player is in the local player's party.
        /// </summary>
        internal readonly bool IsInParty => this.character->IsPartyMember;

        /// <summary>
        ///     Whether or not the player is known to the local player (i.e. in party or friend).
        /// </summary>
        internal readonly bool IsKnownPlayer => this.IsInParty || this.IsFriend;

        /// <summary>
        ///     The level of the player.
        /// </summary>
        internal readonly byte Level => this.character->CharacterData.Level;

        /// <summary>
        ///     The pointer for the character data.
        /// </summary>
        internal readonly nint CharacterPtr => (nint)this.character;

        /// <summary>
        ///     The location of the player.
        /// </summary>
        internal readonly FFXIVClientStructs.FFXIV.Common.Math.Vector3 Position => this.character->GameObject.Position;

        /// <summary>
        ///     Targets the player.
        /// </summary>
        internal readonly void Target() => TargetSystem.Instance()->Target = (GameObject*)this.character;

        /// <summary>
        ///     Opens the examine window for the player.
        /// </summary>
        internal readonly void OpenExamine() => AgentInspect.Instance()->ExamineCharacter(this.character->GameObject.ObjectID);

        /// <summary>
        ///     Opens the character card for the player.
        /// </summary>
        internal readonly void OpenCharaCard() => AgentCharaCard.Instance()->OpenCharaCard((GameObject*)this.character);
    }
}
