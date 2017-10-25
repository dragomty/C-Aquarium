using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              
using XNAMachinationisRatio;                
using XNAMachinationisRatio.AI;            
using System.Timers;



namespace FishORama
{
    
    class SeaHorseMind : AIPlayer
    {
        #region Data Members

        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).\
        private float YDirection;
        private float XDirection;
        private int mSpeed;
        private int rnd;
        private int rspeed;
        private bool randomevent = true;//////random bool which can valid or not
        private int SwimPositionMinus;
        private int SwimPositionPlus;
        private int ScatterPositionY;
        private int ScatterPositionX;
        private bool Position = true;
        private int maxspeed = 15;
        private bool Scatter = false;

        private float rndDirx, rndDiry;
                
    
        #endregion

        #region Properties

        /// <summary>
        /// Set Aquarium in which the mind's behavior should be enacted.
        /// </summary>
        public AquariumToken Aquarium
        {
            set { mAquarium = value; }

        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="pToken">Token to be associated with the mind.</param>
        public SeaHorseMind(X2DToken pToken)
        {
            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
            Random rnd1 = new Random();
            int LeftRight = rnd1.Next(1, 10);

            if (LeftRight > 9)///generating directions
            {
                YDirection = 1;
            }

            else
            {
                YDirection = -1;

            }
            Random rndDir = new Random();
            rndDirx = (float)rndDir.NextDouble();////transform double to float//////////
            Random rndMinDirection = new Random();



            Random rnd2 = new Random();
            int rndMinusDirection = rnd2.Next(1, 10);

            if (rndMinusDirection < 5)
            {
                rndDirx = -1;////generate  x 
            }


            rndDiry = (float)rndDir.NextDouble();
            rndMinusDirection = rnd2.Next(1, 10);

            if (rndMinusDirection < 5)
            {
                rndDiry = -1;////generate y
            }

     
        }


        #endregion
        #region Methods

        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>
        public override void Update(ref GameTime pGameTime)
        {

            Vector3 tokenPosition = this.PossessedToken.Position;


            if (Scatter == true)
            {
                ScatterPositionY = (int)this.PossessedToken.Position.Y;
                ScatterPositionX = (int)this.PossessedToken.Position.X;
                tokenPosition = ScatterBehaviour(tokenPosition);
            }
          
            

            if (Position == true)
            {
                SwimPositionMinus = (int) this.PossessedToken.Position.Y - 100; //minus
                SwimPositionPlus = (int)this.PossessedToken.Position.Y + 100; //plus int
                Position = false;
            }
            

            if (randomevent == true)
            {
                rspeed = gRandom(4, 10);
                mSpeed = rspeed;
            }

            if (mAquarium.ChickenLeg != null)
            {

                Scatter = true;
                tokenPosition = ScatterBehaviour(tokenPosition);
            }

            else if (mAquarium.ChickenLeg == null)
            {
                Random rndDir = new Random();
                rndDirx = (float)rndDir.NextDouble();
                Random rndMinDirection = new Random();



                Random rnd2 = new Random();////////////////////////////2 directions
                int rndMinusDirection = rnd2.Next(1, 10);

                if (rndMinusDirection < 5)
                {
                    rndDirx = -1;////generate  x 
                }


                rndDiry = (float)rndDir.NextDouble();
                rndMinusDirection = rnd2.Next(1, 10);

                if (rndMinusDirection < 5)
                {
                    rndDiry = -1;////generate y
                }
                
                Scatter = false;
                tokenPosition = HorizontalSwimBehaviour(tokenPosition);

            }

        }

        public Vector3 HorizontalSwimBehaviour(Vector3 tokenPosition)
        {
            
            tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;///do it so it wont go -4 speed allows to increase speed
            tokenPosition.Y = tokenPosition.Y + mSpeed * YDirection;


            if (tokenPosition.Y >= SwimPositionPlus)///
            {
               YDirection *= -1;
               tokenPosition.Y = tokenPosition.Y + mSpeed * YDirection;
               this.PossessedToken.Orientation = new Vector3(mFacingDirection, YDirection, this.PossessedToken.Orientation.Z);
            }

            if (tokenPosition.Y <= SwimPositionMinus)
            {
                YDirection *= -1;
                tokenPosition.Y = tokenPosition.Y + mSpeed * YDirection;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, YDirection, this.PossessedToken.Orientation.Z);
            }
            
            if (tokenPosition.X >= 400 || tokenPosition.X <= -400)
            {
                mFacingDirection *= -1;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, YDirection, this.PossessedToken.Orientation.Z);
            }
            this.PossessedToken.Position = tokenPosition;////new
            return tokenPosition;// returns tokenposition
        }

        private int gRandom(int min, int max)//////////////////generating number///////////
        {
            Random rnd = new Random();
            int rspeed = rnd.Next(5, 11);///generates 1 or 2 for generating seahorse speed
            return rspeed;
  
        }

        public Vector3 ScatterBehaviour(Vector3 tokenPosition)
        {
           
           
                tokenPosition.X = tokenPosition.X + maxspeed * rndDirx;//
                tokenPosition.Y = tokenPosition.Y + maxspeed * rndDiry;
           

                if (tokenPosition.Y >= 230 || tokenPosition.Y <= -230)///
               {
                   rndDiry *= 0;
                   rndDirx = 0;
                   Console.WriteLine("cccc");
                   this.PossessedToken.Orientation = new Vector3(rndDirx, rndDiry, this.PossessedToken.Orientation.Z);
               }


                if (tokenPosition.X >= 280 || tokenPosition.X <= -280)
                {
                    rndDirx *= 0;
                    rndDiry = 0;
                    this.PossessedToken.Orientation = new Vector3(rndDirx, rndDiry, this.PossessedToken.Orientation.Z);
                }
                this.PossessedToken.Position = tokenPosition;
                return tokenPosition;// returns tokenposition


        }
    }
}


        #endregion
