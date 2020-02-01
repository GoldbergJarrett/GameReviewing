﻿using GameReviewing.Models;
using GameReviewing.Services.Interfaces;
using GameReviewing.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviewing.Pages
{
    public partial class GameForm : ComponentBase
    {
        [Inject]
        public IGameService GameService { get; set; }
        [Parameter]
        public Game GameParameter { get; set; }
        [Parameter]
        public int Id { get; set; }

        public GameFormViewModel Game { get; set; }
        public Game GameFromId { get; set; }
        public EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            if (Id != 0)
            {
                GameFromId = GameService.GetGameById(Id);

                if(!(GameFromId.Id == 0))
                {
                    Game = new GameFormViewModel { Title = GameFromId.Title };
                }
                else
                {
                    Game = new GameFormViewModel { Title = "Error game with Id:" + Id + " was not found" };
                }
            }
            else if (GameParameter != null)
            {
                Game = new GameFormViewModel() { Title = GameParameter.Title };
            }
            else
            {
                Game = new GameFormViewModel();
            }

            EditContext = new EditContext(Game);
        }

        public void OnSubmit()
        {
            if(Id != 0)
            {
                GameFromId.Title = Game.Title;
            }
            else if(GameParameter != null)
            {
                GameParameter.Title = Game.Title;
            }
            else
            {
                GameService.AddGame(new Game { Id = GameService.NextId, Title = Game.Title });
            }
        }

        public void ImageSelected()
        {

        }
    }
}
