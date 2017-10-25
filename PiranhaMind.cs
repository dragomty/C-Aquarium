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
   
    
     class PiranhaMind : AIPlayer
    {
        #region Data Members

       
        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.
         private AquariumMind pAquarium;

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private int mSpeed = 5;
        private int turns = 0;
        private int xPos;
        private int yPos;
        //Timer timer = new Timer();
        private int rnd;
        private int Value;
        private bool randomevent = true;//////random bool which can valid or not
        private bool dashing = false;////dashing bool
        private int distance = 0;////distance variable increases d
        private float feedingdirx;
        private float feedingdiry;
        private int currenttime = 0;
        private int endtime = 0;
        private int fulltime = 0;
      //  private bool full = false;
        private int fullspeed = 1;

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
        public PiranhaMind(X2DToken pToken)
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
       
            currenttime = DateTime.Now.Second + DateTime.Now.Minute * 60;
           

            if (mAquarium.ChickenLeg == null && endtime < currenttime)
            {
               mSpeed = 5;
               currenttime = 0;
            }

            else if(mAquarium.ChickenLeg != null && endtime > currenttime)
            {
                mSpeed = 5;
                currenttime = 0;
                
            }

            if (mAquarium.ChickenLeg != null && endtime < currenttime)////leg is there
            {
               
               tokenPosition = Feeding(tokenPosition);
            }

            else 
            {

            tokenPosition = HungrySwimBehaviour(tokenPosition);
            
            }


        }

        public Vector3 FullSwimBehaviour(Vector3 tokenPosition)
        {
            tokenPosition.X = tokenPosition.X + fullspeed * mFacingDirection;///do it so it wont go -4 speed allows to increase speed
            if (tokenPosition.X > 400 || tokenPosition.X < -400)
            {
    
                mFacingDirection *= -1;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);
            }
            this.PossessedToken.Position = tokenPosition;
            return tokenPosition;// returns tokenposition
        }

        
            public Vector3 HungrySwimBehaviour(Vector3 tokenPosition)
            {
                    tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;
                    if (tokenPosition.X > 400 || tokenPosition.X < -400)
                    {
                       
                        mFacingDirection *= -1;
                        this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);                
                    }

                    
                    this.PossessedToken.Position = tokenPosition;
                    return tokenPosition;// returns tokenposition
            }

            public Vector3 Feeding(Vector3 tokenPosition)
            {
                double x = (mAquarium.ChickenLeg.Position.X - tokenPosition.X);
                double y  = (mAquarium.ChickenLeg.Position.Y - tokenPosition.Y);

                feedingdirx = (float) (x / Math.Sqrt(x * x + y * y));// generate to swim towards leg x
                feedingdiry = (float) (y / Math.Sqrt(x * x + y * y));//
               

            tokenPosition.X = tokenPosition.X + mSpeed * feedingdirx;
            tokenPosition.Y = tokenPosition.Y + mSpeed * feedingdiry;
            currenttime = DateTime.Now.Second + DateTime.Now.Minute * 60;
        

            this.PossessedToken.Orientation = new Vector3(feedingdirx, feedingdiry, this.PossessedToken.Orientation.Z);

            if (tokenPosition.X > mAquarium.ChickenLeg.Position.X - 10 && tokenPosition.X < mAquarium.ChickenLeg.Position.X + 10)
              {
                  mAquarium.RemoveChickenLeg();
                  Console.WriteLine("removed");
                  mAquarium.ChickenLeg = null;
                  currenttime = PiranhaTime();
                  endtime = PiranhaTime() + 5;//////5 sec passed
                  mSpeed = 1;
                 // full = true;
             }

            else if (tokenPosition.Y > mAquarium.ChickenLeg.Position.Y - 10 && tokenPosition.Y < mAquarium.ChickenLeg.Position.Y + 10)
            {
                mAquarium.RemoveChickenLeg();
                Console.WriteLine("removed");
                mAquarium.ChickenLeg = null;
                currenttime = PiranhaTime();
                endtime = PiranhaTime() + 5;
                mSpeed = 1;
            }
                this.PossessedToken.Position = tokenPosition;
                return tokenPosition;
            }

             private int PiranhaTime()
            {
                currenttime = DateTime.Now.Second + DateTime.Now.Minute * 60;
                fulltime = DateTime.Now.Second + DateTime.Now.Minute * 60;
               
                return currenttime;
              
            }


     }
}


#endregion
