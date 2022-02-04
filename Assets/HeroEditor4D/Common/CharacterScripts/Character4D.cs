﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor4D.Common.CommonScripts;
using Assets.HeroEditor4D.FantasyInventory.Scripts.Data;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.CharacterScripts
{
	/// <summary>
	/// Controls 4 characters (for each direction).
	/// </summary>
	public class Character4D : Character4DBase
    {
		public SpriteCollection SpriteCollection => Parts[0].SpriteCollection;
        public AnimationManager AnimationManager;

        public void OnValidate()
		{
			Parts = new List<CharacterBase> { Front, Back, Left, Right };
			Parts.ForEach(i => i.BodyRenderers.ForEach(j => j.color = BodyColor));
			Parts.ForEach(i => i.EarsRenderers.ForEach(j => j.color = BodyColor));
		}

        public void Start()
        {
            var stateHandler = Animator.GetBehaviours<StateHandler>().SingleOrDefault(i => i.Name == "Death");

            if (stateHandler)
            {
                stateHandler.StateExit.AddListener(() => SetExpression("Default"));
            }
        }

		public override void Initialize()
		{
			Parts.ForEach(i => i.Initialize());
		}

		public override void CopyFrom(Character4DBase character)
		{
			for (var i = 0; i < Parts.Count; i++)
			{
				Parts[i].CopyFrom(character.Parts[i]);
			}
		}

		public override string ToJson()
		{
		    return Front.ToJson();
		}

		public override void LoadFromJson(string json, bool silent)
		{
		    Parts.ForEach(i => i.LoadFromJson(json, silent));
		}

        public Vector2 Direction { get; private set; }

		public void SetDirection(Vector2 direction)
		{
            if (Direction == direction) return;

			Direction = direction;

            if (Direction == Vector2.zero)
            {
                Parts.ForEach(i => i.SetActive(true));
                Shadows.ForEach(i => i.SetActive(true));

                Parts[0].transform.localPosition = Shadows[0].transform.localPosition = new Vector3(0, -1.25f);
                Parts[1].transform.localPosition = Shadows[1].transform.localPosition = new Vector3(0, 1.25f);
                Parts[2].transform.localPosition = Shadows[2].transform.localPosition = new Vector3(-1.5f, 0);
                Parts[3].transform.localPosition = Shadows[3].transform.localPosition = new Vector3(1.5f, 0);

                return;
            }

			Parts.ForEach(i => i.transform.localPosition = Vector3.zero);
			Shadows.ForEach(i => i.transform.localPosition = Vector3.zero);

			int index;

			if (direction == Vector2.left)
			{
				index = 2;
			}
			else if (direction == Vector2.right)
			{
				index = 3;
			}
			else if (direction == Vector2.up)
			{
				index = 1;
			}
			else if (direction == Vector2.down)
			{
				index = 0;
			}
            else
			{
				throw new NotSupportedException();
			}

			for (var i = 0; i < Parts.Count; i++)
			{
                Parts[i].SetActive(i == index);
				Shadows[i].SetActive(i == index);
			}
		}

        public void SetExpression(string expression)
        {
            Parts.ForEach(i => ((Character) i).SetExpression(expression));
        }

		#region Setup Examples

		public void EquipArmor(List<Sprite> sprites)
        {
            Armor = sprites;
			Initialize();
        }

        public void EquipArmor(SpriteGroupEntry entry)
        {
            EquipArmor(entry?.Sprites);
        }

        public void EquipArmor(Item item)
        {
            if (item == null) EquipArmor(null as SpriteGroupEntry);
            else EquipArmor(SpriteCollection.Armor.Single(i => i.Path == item.Params.Path));
        }

        public void EquipHelmet(List<Sprite> sprites)
        {
            Helmet = sprites;
            Initialize();
        }

        public void EquipHelmet(SpriteGroupEntry entry)
        {
            Parts.ForEach(i => i.HideEars = entry != null && !entry.Tags.Contains("ShowEars")); // Helmet hides ears by default.
            Parts.ForEach(i => i.CropHair = entry != null && !entry.Tags.Contains("FullHair")); // Helmet hides hair by default.
            EquipHelmet(entry?.Sprites);
        }

        public void EquipHelmet(Item item)
        {
            if (item == null) EquipHelmet(null as SpriteGroupEntry);
            else EquipHelmet(SpriteCollection.Armor.Single(i => i.Path == item.Params.Path.Replace("Helmet/", "Armor/").Replace(".Helmet", "")));
        }

		public void EquipShield(List<Sprite> sprites)
        {
            Shield = sprites;

            switch (WeaponType)
            {
                case WeaponType.Melee1H:
                    break;
                case WeaponType.Paired:
                    SecondaryWeapon = null;
                    break;
                default:
                    PrimaryWeapon = null;
                    SecondaryWeapon = null;
                    CompositeWeapon = null;
                    break;
            }

            WeaponType = WeaponType.Melee1H;
            Initialize();
		}

        public void EquipShield(SpriteGroupEntry entry)
        {
            EquipShield(entry?.Sprites);
        }

        public void EquipShield(Item item)
        {
            EquipShield(SpriteCollection.Shield.SingleOrDefault(i => i.FullName == item.Id));
        }

        public void EquipMeleeWeapon1H(Sprite sprite)
        {
            PrimaryWeapon = sprite;
            CompositeWeapon = null;
            WeaponType = WeaponType.Melee1H;
            Initialize();
		}

        public void EquipMeleeWeapon1H(SpriteGroupEntry entry)
        {
            EquipMeleeWeapon1H(entry?.Sprite);
        }

        public void EquipMeleeWeapon1H(Item item)
        {
            EquipMeleeWeapon1H(SpriteCollection.MeleeWeapon1H.SingleOrDefault(i => i.FullName == item.Id));
        }

		public void EquipMeleeWeapon2H(Sprite sprite)
        {
            PrimaryWeapon = sprite;
            Shield = CompositeWeapon = null;
            WeaponType = WeaponType.Melee2H;
            Initialize();
		}

        public void EquipMeleeWeapon2H(SpriteGroupEntry entry)
        {
            EquipMeleeWeapon2H(entry?.Sprite);
        }

        public void EquipMeleeWeapon2H(Item item)
        {
            EquipMeleeWeapon2H(SpriteCollection.MeleeWeapon2H.SingleOrDefault(i => i.FullName == item.Id));
        }

        public void EquipMeleeWeaponPaired(Sprite primary, Sprite secondary)
        {
            PrimaryWeapon = primary;
            SecondaryWeapon = secondary;
            CompositeWeapon = null;
            WeaponType = WeaponType.Paired;
            Initialize();
        }

		public void EquipBow(List<Sprite> sprites)
        {
            CompositeWeapon = sprites;
            PrimaryWeapon = null;
            Shield = null;
            WeaponType = WeaponType.Bow;
            Initialize();
		}

        public void EquipBow(SpriteGroupEntry entry)
        {
            EquipBow(entry?.Sprites);
        }

        public void EquipBow(Item item)
        {
            EquipBow(SpriteCollection.Bow.SingleOrDefault(i => i.FullName == item.Id));
        }

        public void EquipCrossbow(List<Sprite> sprites)
        {
            CompositeWeapon = sprites;
            PrimaryWeapon = null;
            Shield = null;
            WeaponType = WeaponType.Crossbow;
            Initialize();
        }

        public void EquipSecondaryFirearm(List<Sprite> sprites)
        {
            SecondaryWeapon = sprites?[1];
            CompositeWeapon = null;
            Shield = null;
            WeaponType = WeaponType.Paired;
            Initialize();
        }

        public void EquipSupply(Sprite sprite)
        {
            PrimaryWeapon = sprite;
            CompositeWeapon = null;
            WeaponType = WeaponType.Melee1H;
            Initialize();
        }

        public void SetBody(List<Sprite> sprites)
        {
            Body = sprites;
            Initialize();
        }

        public void SetBody(SpriteGroupEntry entry)
        {
            SetBody(entry?.Sprites);
        }

        public void SetEars(List<Sprite> sprites)
        {
            Ears = sprites;
            Initialize();
		}

        public void SetEars(SpriteGroupEntry entry)
        {
            SetEars(entry?.Sprites);
        }

        public void SetEyebrows(List<Sprite> sprites)
        {
            Eyebrows = sprites;
            Initialize();
        }

        public void SetEyebrows(SpriteGroupEntry entry)
        {
            SetEyebrows(entry?.Sprites);
        }

        public void SetEyes(List<Sprite> sprites)
        {
            Eyes = sprites;
            Initialize();
        }

        public void SetEyes(SpriteGroupEntry entry)
        {
            SetEyes(entry?.Sprites);
        }

        public void SetHair(List<Sprite> sprites, string eqName = null)
        {
            Hair = sprites;

            Parts.ForEach(i => i.HideEars |= sprites != null && eqName != null && eqName.Contains("[HideEars]")); // Hair doesn't hide ears by default.
            
            Initialize();
        }

        public void SetHair(SpriteGroupEntry entry)
        {
            SetHair(entry?.Sprites, entry?.Name);
        }

        public void SetBeard(List<Sprite> sprites)
        {
            Beard = sprites;
            Initialize();
        }

        public void SetBeard(SpriteGroupEntry entry)
        {
            SetBeard(entry?.Sprites);
        }

        public void SetMouth(List<Sprite> sprites)
        {
            Mouth = sprites;
            Initialize();
        }

        public void SetMouth(SpriteGroupEntry entry)
        {
            SetMouth(entry?.Sprites);
        }

        public void SetMakeup(List<Sprite> sprites)
        {
            Makeup = sprites;
            Initialize();
        }

        public void SetMakeup(SpriteGroupEntry entry)
        {
            SetMakeup(entry?.Sprites);
        }

        public void SetMask(List<Sprite> sprites)
        {
            Mask = sprites;
            Initialize();
        }

        public void SetMask(SpriteGroupEntry entry)
        {
            SetMask(entry?.Sprites);
        }

        public void SetEarrings(List<Sprite> sprites)
        {
            Earrings = sprites;
            Initialize();
        }

        public void SetEarrings(SpriteGroupEntry entry)
        {
            SetEarrings(entry?.Sprites);
        }

        #endregion
    }
}