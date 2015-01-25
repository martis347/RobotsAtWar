namespace RobotsAtWar.Server
{
    public class BattleFieldSingleton
    {

        private static BattleField _battleField;

        public static BattleField BattleField
        {
            get
            {
                if (_battleField == null)
                {
                    _battleField = new BattleField();
                }
                return _battleField;
                
            }
        }
    }
}
