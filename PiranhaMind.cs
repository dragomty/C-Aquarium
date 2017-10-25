using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.
using System.Timers;




/* LERNING PILL: XNAMachinationisRatio Engine
 * XNAMachinationisRatio is an engine that allows implementing
 * simulations and games based on XNA, simplifying the use of XNA
 * and adding features not directly available in XNA.
 * XNAMachinationisRatio is a work in progress.
 * The engine works "under the hood", taking care of many features
 * of an interactive simulation automatically, thus minimizing
 * the amount of code that developers have to write.
 * 
 * In order to use the engine, the application main class (Kernel, in the
 * case of FishO'Rama) creates, initializes and stores
 * an instance of class Engine in one of its data members.
 * 
 * The classes comprised in the  XNA Machinationis Ratio engine and the
 * related functionalities can be accessed from any of your XNA project
 * source code files by adding appropriate 'using' statements at the beginning of
 * the file. 
 * 
 */

namespace FishORama
{
    /* LEARNING PILL: Token behaviors in the XNA Machinationis Ratio engine
     * Some simulation tokens may need to enact specific behaviors in order to
     * participate in the simulation. The XNA Machinationis Ratio engine
     * allows a token to enact a behavior by associating an artificial intelligence
     * mind to it. Mind objects are created from subclasses of the class AIPlayer
     * included in the engine. In order to associate a mind to a token, a new
     * mind object must be created, passing to the constructor of the mind a reference
     * of the object that must be associated with the mind. This must be done in
     * the DefaultProperties method of the token.
     * 
     * Hence, every time a new tipe of AI mind is required, a new class derived from
     * AIPlayer must be created, and an instance of it must be associated to the
     * token classes that need it.
     * 
     * Mind objects enact behaviors through the method Update (see below for further details). 
     */
     class PiranhaMind : AIPlayer
    {
        #region Data Members

        // This mind needs to interact with the token which it possesses, 
        // since it needs to know where are the aquarium's boundaries.
        // Hence, the mind needs a "link" to the aquarium, which is why it stores in
        // an instance variable a reference to its aquarium.
        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.
         private AquariumMind pAquarium;

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private int mSpeed = 5;//da speed////
        private int turns = 0;
        private int xPos;////////////positions
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
            /* LEARNING PILL: associating a mind with a token
             * In order for a mind to control a token, it must be associated with the token.
             * This is done when the mind is constructed, using the method Possess inherited
             * from class AIPlayer.
             */
            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
            //int turns = 1;///turns variable?
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
         //   tokenPosition = HungrySwimBehaviour(tokenPosition);////calls normalswim on every update
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

        public Vector3 FullSwimBehaviour(Vector3 tokenPosition)//this is the normal swim
        {
            tokenPosition.X = tokenPosition.X + fullspeed * mFacingDirection;///do it so it wont go -4 speed allows to increase speed
            if (tokenPosition.X > 400 || tokenPosition.X < -400)
            {
    
                mFacingDirection *= -1;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);
            }
            this.PossessedToken.Position = tokenPosition;////new
            return tokenPosition;// returns tokenposition
        }

        
            public Vector3 HungrySwimBehaviour(Vector3 tokenPosition)//this is the normal swim
            {
                    tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;///do it so it wont go -4 speed allows to increase speed
                    if (tokenPosition.X > 400 || tokenPosition.X < -400)
                    {
                        //gRandom(1, 5);////////everytime the side is hit generate number
                        mFacingDirection *= -1;
                        this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);                
                    }

                    
                    this.PossessedToken.Position = tokenPosition;////new
                    return tokenPosition;// returns tokenposition
            }

            public Vector3 Feeding(Vector3 tokenPosition)
            {
                double x = (mAquarium.ChickenLeg.Position.X - tokenPosition.X);
                double y  = (mAquarium.ChickenLeg.Position.Y - tokenPosition.Y);

                feedingdirx = (float) (x / Math.Sqrt(x * x + y * y));// generate to swim towards leg x
                feedingdiry = (float) (y / Math.Sqrt(x * x + y * y));//
                //  Console.WriteLine("hhh");
                //   Console.WriteLine("x={0}", feedingdirx);
                //  Console.WriteLine("y ={0}", feedingdiry);
                

            tokenPosition.X = tokenPosition.X + mSpeed * feedingdirx;///do it so it wont go -4 speed allows to increase speed
            tokenPosition.Y = tokenPosition.Y + mSpeed * feedingdiry;
            currenttime = DateTime.Now.Second + DateTime.Now.Minute * 60;
        

            this.PossessedToken.Orientation = new Vector3(feedingdirx, feedingdiry, this.PossessedToken.Orientation.Z);

            if (tokenPosition.X > mAquarium.ChickenLeg.Position.X - 10 && tokenPosition.X < mAquarium.ChickenLeg.Position.X + 10)///////was tokenpositionx
              {
                  mAquarium.RemoveChickenLeg();//////////////remove from aquarium
                  Console.WriteLine("removed");
                  mAquarium.ChickenLeg = null;
                  currenttime = PiranhaTime();
                  endtime = PiranhaTime() + 5;//////5 sec passed
                  mSpeed = 1;
                 // full = true;
             }

            else if (tokenPosition.Y > mAquarium.ChickenLeg.Position.Y - 10 && tokenPosition.Y < mAquarium.ChickenLeg.Position.Y + 10)///////was tokenpositionx
            {
                mAquarium.RemoveChickenLeg();//////////////remove from aquarium
                Console.WriteLine("removed");
                mAquarium.ChickenLeg = null;
                currenttime = PiranhaTime();
                endtime = PiranhaTime() + 5;////////////////
                mSpeed = 1;
            }
                this.PossessedToken.Position = tokenPosition;
                return tokenPosition;
            }

             private int PiranhaTime()////////////////////passed time method ??????
            {
                currenttime = DateTime.Now.Second + DateTime.Now.Minute * 60;
                fulltime = DateTime.Now.Second + DateTime.Now.Minute * 60;
                //Console.WriteLine("time passed" + currenttime);
                return currenttime;
               // return fulltime;
            }


     }
}


#endregion