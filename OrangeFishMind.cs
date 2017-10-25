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
    
    class OrangeFishMind : AIPlayer
    {
        #region Data Members

       
        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private int mSpeed = 1;
        private int turns = 0;
        private int xPos;
        private int yPos;
        private int rnd;
        private int Value;
        private bool randomevent = true;//////random bool which can valid or not
        private bool dashing = false;////dashing bool
        private int distance = 0;////distance variable increases during speed up
        private bool behaviourdash = false;
        private bool NormalSwim = true;///normal bool
        private bool accel = false;///acceleration bool
        private bool accelbehaviour = false;
        private bool sink = true;
        private int currenttime;
        private int endtime;
        private bool whatevertime = false;
        private bool timestuff = false;
        public float fishposx;
        public float fishposy;
                   

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
        public OrangeFishMind(X2DToken pToken)
        {
           
            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
            
            
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
            fishposx = tokenPosition.X;
            fishposy = tokenPosition.Y;

            if ((!behaviourdash)&&(!accelbehaviour))
            {
               Value = gRandom(1, 5);
               if (Value == 4)
               {
                   accelbehaviour = true;
                   accel = true;
                   endtime = GetCurrentTime() + 15;
                   Console.WriteLine("number was 4");
               }
               else if (Value == 2)
               {
                   behaviourdash = true;
                   Console.WriteLine("number was 2"); 
               }
            }
            


            if (accelbehaviour == true)
            {
               
                whatevertime = true;
                currenttime = GetCurrentTime(); //getting current time
                timestuff = true;
                tokenPosition = Accelerate(tokenPosition);
            }

            else if (behaviourdash == true)///behaviour connect this with number
            {
                Console.WriteLine("dash was true and it dashes");
                dashing = true;//added here from value == 2
               tokenPosition = Dash(tokenPosition);
            }
            else
            {
              
                tokenPosition = NormalSwimBehaviour(tokenPosition);
                distance = 0;//reset distance after dash
                mSpeed = 1;
            }
            this.PossessedToken.Position = tokenPosition;

        }



        public Vector3 NormalSwimBehaviour(Vector3 tokenPosition)//this is the normal swim
        {
            tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;///do it so it wont go -4 speed allows to increase speed
            if (tokenPosition.X >= 400 || tokenPosition.X <= -400)
            {
                mFacingDirection *= -1;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);                
            }
            return tokenPosition;// returns tokenposition
        }

        public Vector3 Dash (Vector3 tokenPosition)
        {
            tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;
            distance = distance + mSpeed;
            
            if (tokenPosition.X >= 400 || tokenPosition.X <= -400)
            {
                mFacingDirection *= -1;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);
            }

            if (dashing == true)
            {
 
                mSpeed = mSpeed + 10;
                dashing = false;
                
            }

            if (distance >= 250)
            {
             
                dashing = false;
                behaviourdash = false;
                mSpeed = 1;
                
            }
            return tokenPosition;
        }
         public Vector3 Accelerate(Vector3 tokenPosition)////acceleration 
         {
         
                tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;///allows +=
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);
          
                if (tokenPosition.X >= 400 || tokenPosition.X <= -400)
                {
                    mFacingDirection *= -1;
                    this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);
                }

                if (accel == true)
                {
                    Console.WriteLine("accelerates");
                    if (mSpeed < 12)
                    {
                        mSpeed += 1;
                    }

                    else
                    {
                        accel = false;
                    }

     
                }

                else if (endtime < currenttime)//if 15 sec passed and speed is not 3 decrease speed 1 by 1 ///////////NOTE use & OR &&///////////// if timepassed is less than 15 accelerate
                {
                    Console.WriteLine("bs");

                    if (mSpeed > 1)////looping and decreasing speed
                    {
                        mSpeed -= 1;
                    }
                    else
                    {
                        whatevertime = false;
                        accelbehaviour = false;
                        Console.WriteLine("stops accelerating");
                    }
                }
               return tokenPosition;/////return always the last 
        }

  
            public int gRandom(int min, int max)//////////////////generating number///////////
            {
                    Random rnd = new Random();
                    int Value = rnd.Next(1, 5);////generates from 1 to 4
                    return Value;
                   
            }

            private int GetCurrentTime()
            {
                int currenttime = DateTime.Now.Second + DateTime.Now.Minute * 60;
                return currenttime;
            }          
                              
    }
}


#endregion

  
