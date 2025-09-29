using Snake.UpdateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.MVVM.View
{
    internal class GamebaordView : IUpdateEntity
    {
        private static GamebaordView _instance;
        public static GamebaordView Instance
        {
            get
            {
                if(GamebaordView._instance == null)
                {
                    GamebaordView._instance = new GamebaordView();
                }

                return GamebaordView.Instance;
            }
        }

        private GamebaordView() 
        { 
        
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
