using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Graphics;
using DinaCSharp.Inputs;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Scenes;

using Donjon_100_Pas.Core;
using Donjon_100_Pas.Core.Datas.Characters;
using Donjon_100_Pas.Core.Datas.Items;
using Donjon_100_Pas.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Donjon_100_Pas.GameMechanics.Scenes
{
    public class InventoryScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private const float PLAYER_LABEL_OFFSET_Y = 15f;
        private const float PLAYER_LABEL_OFFSET_X = 15f;
        private const float PLAYER_GROUP_OFFSET_X = 25f;
        private readonly Vector2 PLAYER_PANEL_DIMENSIONS = new Vector2(192, 384);

        private const float EQUIPMENT_OFFSET_X = 10f;
        private readonly Vector2 EQUIPMENT_DIMENSIONS = new Vector2(96, 96);

        private readonly Vector2 BUTTON_NEXT_DIMENSIONS = new Vector2(136, 80);

        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
        private readonly ResourceManager _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.GameResourceManager);
        //private readonly PlayerController _playerController = ServiceLocator.Get<PlayerController>(ProjectServiceKeys.PlayerController);

        private Player _player;
        private Group _playerGroup;

        private Text _attackText;
        private Text _defenseText;
        private Text _healthText;
        private Text _manaText;
        private Text _goldText;

        private Group _equipmentGroup;

        private Button _backButton;

        public override void Load()
        {
            _player = ServiceLocator.Get<Player>(ProjectServiceKeys.Player);
            _player.OnWeaponChanged += OnWeaponChanged;
            _player.OnArmorChanged += OnArmorChanged;
            _player.OnStatsChanged += OnStatsChanged;

            _playerGroup = CreatePlayerGroup();
            _playerGroup.Position = new Vector2(ScreenDimensions.X * 1 / 6, ScreenDimensions.Y * 1 / 4);

            CreateEquipmentGroup();

            _backButton = new Button(position: new Vector2(ScreenDimensions.X * 4 / 5, ScreenDimensions.Y * 7 / 8),
                                     dimensions: UIScaler.Scale(BUTTON_NEXT_DIMENSIONS),
                                     font: _fontManager.Load(FontKeys.Inventory_Button_Text),
                                     content: "UI_BACK",
                                     textColor: PaletteColors.Inventory_Button_Text,
                                     backgroundImage: _resourceManager.Load<Texture2D>(GameResourceKeys.Button_Next),
                                     onClick: ReturnToCity, onHover: OnButtonNextHovered);
        }
        public override void Reset()
        {
        }
        public override void Update(GameTime gametime)
        {
            _backButton?.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _playerGroup?.Draw(spritebatch);
            _equipmentGroup?.Draw(spritebatch);
            _backButton?.Draw(spritebatch);
        }


        private void OnWeaponChanged(Weapon weapon)
        {
            CreateEquipmentGroup();
        }
        private void OnArmorChanged(Armor armor)
        {
            CreateEquipmentGroup();
        }
        private void OnStatsChanged()
        {
            _healthText.Content = $"{_player.Health} / {_player.MaxHealth}";
            _attackText.Content = $"{_player.Attack}";
            _defenseText.Content = $"{_player.Defense}";
            _manaText.Content = $"{_player.Mana} / {_player.MaxMana}";
            _goldText.Content = $"{_player.Gold}";
        }
        private Group CreatePlayerGroup()
        {
            Group group = new();

            Panel playerPanel = new Panel(position: default, dimensions: UIScaler.Scale(PLAYER_PANEL_DIMENSIONS), image: _player.Texture);
            group.Add(playerPanel);

            var pos = new Vector2(0, playerPanel.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));

            var classGroup = CreatePlayerStatGroup("PLAYER_CLASS_LABEL", _player.Name, UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _);
            var levelText = new Text(_fontManager.Load(FontKeys.Player_Value), $" ({_player.Level})", PaletteColors.Player_Value,
                                     new Vector2(classGroup.Dimensions.X, 0));
            classGroup.Add(levelText);
            classGroup.Position = pos;
            group.Add(classGroup);

            // Health
            pos += new Vector2(0, classGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var strHealth = $"{_player.Health} / {_player.MaxHealth}";
            var healthGroup = CreatePlayerStatGroup("PLAYER_HEALTH_LABEL", strHealth, UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _healthText);
            healthGroup.Position = pos;
            group.Add(healthGroup);

            // Mana
            pos += new Vector2(0, healthGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var strMana = $"{_player.Mana} / {_player.MaxMana}";
            var manaGroup = CreatePlayerStatGroup("PLAYER_MANA_LABEL", strMana, UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _manaText);
            manaGroup.Position = pos;
            group.Add(manaGroup);

            // Attack
            pos += new Vector2(0, manaGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var attackGroup = CreatePlayerStatGroup("PLAYER_ATTACK_LABEL", _player.Attack.ToString(), UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _attackText);
            attackGroup.Position = pos;
            group.Add(attackGroup);

            // Defense
            pos += new Vector2(0, attackGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var defenseGroup = CreatePlayerStatGroup("PLAYER_DEFENSE_LABEL", _player.Defense.ToString(), UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _defenseText);
            defenseGroup.Position = pos;
            group.Add(defenseGroup);

            // Gold
            pos += new Vector2(0, defenseGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var goldGroup = CreatePlayerStatGroup("PLAYER_GOLD_LABEL", _player.Gold.ToString(), UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _goldText);
            goldGroup.Position = pos;
            group.Add(goldGroup);

            return group;
        }
        private Group CreatePlayerStatGroup(string strLabel, string strText, float offsetX, out Text valueText)
        {
            var labelFont = _fontManager.Load(FontKeys.Player_Label);
            var valueFont = _fontManager.Load(FontKeys.Player_Value);

            return CreateLabelAndText(strLabel, PaletteColors.Player_Label, labelFont, strText, PaletteColors.Player_Value, valueFont, offsetX, out valueText);
        }
        private void CreateEquipmentGroup()
        {
            _equipmentGroup?.Dispose();
            _equipmentGroup = CreateWeaponAndArmorGroup();
            _equipmentGroup.Position = _playerGroup.Position + new Vector2(_playerGroup.Dimensions.X + UIScaler.Scale(PLAYER_GROUP_OFFSET_X),
                                                                           UIScaler.Scale(PLAYER_PANEL_DIMENSIONS.Y) - _equipmentGroup.Dimensions.Y);
        }
        private Group CreateWeaponAndArmorGroup()
        {
            Group group = new();

            var weapon = _player.Weapon;
            if (_player.Weapon == null)
            {
                weapon = new Weapon(name: nameof(WeaponKeys.DefaultWeapon).ToUpperInvariant(),
                                    texture: _resourceManager.Load<Texture2D>(WeaponKeys.DefaultWeapon),
                                    bonuses: []);
            }
            var weaponGroup = CreateEquipmentGroup(weapon);
            group.Add(weaponGroup);

            var armor = _player.Armor;
            if (_player.Armor == null)
            {
                armor = new Armor(name: nameof(ArmorKeys.DefaultArmor).ToUpperInvariant(),
                                  texture: _resourceManager.Load<Texture2D>(ArmorKeys.DefaultArmor),
                                  bonuses: []);
            }
            var armorGroup = CreateEquipmentGroup(armor);
            armorGroup.Position = new Vector2(0, weaponGroup.Dimensions.Y + UIScaler.Scale(EQUIPMENT_OFFSET_X));
            group.Add(armorGroup);

            return group;
        }
        private Group CreateEquipmentGroup(Item equipment)
        {
            Group group = new();

            var panel = new Panel(default, UIScaler.Scale(EQUIPMENT_DIMENSIONS), equipment.Texture, 3, true, 5)
            { BorderColor = PaletteColors.Equipment_Border };
            group.Add(panel);

            var bonusFont = _fontManager.Load(FontKeys.Equipment_Bonus_Text);
            var equipmentNameFont = _fontManager.Load(FontKeys.Equipment_Name);

            Color equipmentColor = equipment.Rarity switch
            {
                Rarity.Common => PaletteColors.Equipment_Name_Common,
                Rarity.Uncommon => PaletteColors.Equipment_Name_Uncommon,
                Rarity.Rare => PaletteColors.Equipment_Name_Rare,
                Rarity.Elite => PaletteColors.Equipment_Name_Elite,
                _ => PaletteColors.Equipment_Name_Junk
            };
            var nameText = new Text(equipmentNameFont, equipment.Name, equipmentColor,
                                    new Vector2(panel.Dimensions.X + UIScaler.Scale(EQUIPMENT_OFFSET_X), 0));
            group.Add(nameText);

            // TODO: Ajouter les bonus
            var pos = nameText.Position;
            foreach (var bonus in equipment.Bonuses)
            {
                pos += new Vector2(0, bonusFont.LineSpacing);
                var bonusText = new Text(bonusFont, bonus.GetDescription(_player.Attack), PaletteColors.Equipment_Bonus_Text, pos);
                group.Add(bonusText);
            }
            return group;
        }
        private static Group CreateLabelAndText(string strLabel, Color labelColor, SpriteFont labelFont,
                                                string strText, Color textColor, SpriteFont textFont,
                                                float offsetX, out Text valueText)
        {
            Group group = new();

            var labelText = new Text(labelFont, strLabel, labelColor);
            group.Add(labelText);

            valueText = new Text(textFont, strText, textColor);
            valueText.Position = new Vector2(labelText.Dimensions.X + offsetX, (labelText.Dimensions.Y - valueText.Dimensions.Y) / 2);
            group.Add(valueText);

            return group;
        }
        private void ReturnToCity(Button button)
        {
            SetCurrentScene(ProjectSceneKeys.CityScene);
        }
        private void OnButtonNextHovered(Button button)
        {
            button.TextColor = PaletteColors.Inventory_Button_Text_Hovered;
        }
    }
}
