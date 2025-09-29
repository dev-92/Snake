using Snake.UpdateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.MVVM.ViewModel
{
    internal class GameboardViewModel : IUpdateEntity
    {
        private static GameboardViewModel? _instance;
        public static GameboardViewModel Instance
        {
            get
            {
                if(GameboardViewModel._instance == null)
                {
                    GameboardViewModel._instance = new GameboardViewModel();
                }

                return GameboardViewModel._instance;
            }
        }

        private GameboardViewModel() 
        { 
        
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
