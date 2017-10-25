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
    class OrangeFishMind : AIPlayer
    {
        #region Data Members

        // This mind needs to interact with the token which it possesses, 
        // since it needs to know where are the aquarium's boundaries.
        // Hence, the mind needs a "link" to the aquarium, which is why it stores in
        // an instance variable a reference to its aquarium.
        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private int mSpeed = 1;//da speed////
        private int turns = 0;
        private int xPos;////////////positions
        private int yPos;
        //Timer timer = new Timer();
        private int rnd;
        private int Value;
        private bool randomevent = true;//////random bool which can valid or not
        private bool dashing = false;////dashing bool
        private int distance = 0;////distance variable increases during speed up
       // DateTime oldtime = DateTime.Now;//////////////datetime now  
        //DateTime acceltime = DateTime.Now.AddSeconds(15);
        private bool behaviourdash = false;///false because u need a number
        private bool NormalSwim = true;///normal bool
        private bool accel = false;///acceleration bool
        private bool accelbehaviour = false;
        private bool sink = true;
        private int currenttime;
        private int endtime;
        private bool whatevertime = false;
        private bool timestuff = false;
        //public BubbleToken[] bubble = BubbleTok
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

        /* LEARNING PILL: The AI update method.
         * Mind objects enact behaviors through the method Update. This method is
         * automatically invoked by the engine, periodically, 'under the hood'. This can be
         * be better understood that the engine asks to all the available AI-based tokens:
         * "Would you like to do anything at all?" And this 'asking' is done through invoking
         * the Update method of each mind available in the system. The response is the execution
         * of the Update method of each mind , and all the methods possibly triggered by Update.
         * 
         * Although the Update method could invoke other methods if needed, EVERY
         * BEHAVIOR STARTS from Update. If a behavior is not directly coded in Updated, or in
         * a method invoked by Update, then it is IGNORED.
         * 
         */
        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>
        public override void Update(ref GameTime pGameTime)
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            //tokenPosition = NormalSwimBehaviour(tokenPosition);/////calls normalswim on every update
            fishposx = tokenPosition.X;
            fishposy = tokenPosition.Y;

            if ((!behaviourdash)&&(!accelbehaviour))
            //if (randomevent == true)
            {
               Value = gRandom(1, 5);
               if (Value == 4)
               {
                   accelbehaviour = true;
                   accel = true;
                   endtime = GetCurrentTime() + 15;////////////////
                   Console.WriteLine("number was 4");
               }
               else if (Value == 2)
               {
                   behaviourdash = true;
                   Console.WriteLine("number was 2"); 
               }
            }
            

            /*else //(accelbehaviour == false)
            {
                Console.WriteLine("acceleration was false and it went back to normal");
                //randomevent = true;
                whatevertime = false;
                timestuff = false;
                tokenPosition =  NormalSwimBehaviour(tokenPosition); 
            }*/

            /*if (Value == 2)
            {
                behaviourdash = true;
                randomevent = false;///turn off when right number is found
               // Console.WriteLine("number was 2");  
            }*/

            if (accelbehaviour == true)///////////////////////////////////////////
            {
                //Console.WriteLine("acceleration was true");
                whatevertime = true;
                currenttime = GetCurrentTime(); //getting current time
                //randomevent = false;
                timestuff = true;
                tokenPosition = Accelerate(tokenPosition);
            }

            else if (behaviourdash == true)///behaviour connect this with number
            {
                Console.WriteLine("dash was true and it dashes");
                dashing = true;//added here from value == 2
               tokenPosition = Dash(tokenPosition);///was possessedToken.Position
            }
            else
            {
                //Console.WriteLine("Dash was false and it went to normal");
                //randomevent = true;
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
                //gRandom(1, 5);////////everytime the side is hit generate number
                mFacingDirection *= -1;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);                
            }
            return tokenPosition;// returns tokenposition
        }

        public Vector3 Dash (Vector3 tokenPosition)
        {
            tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;///newly added
            distance = distance + mSpeed;////////distance and speed connnected math stuff
            
            if (tokenPosition.X >= 400 || tokenPosition.X <= -400)
            {
                mFacingDirection *= -1;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);
            }

            if (dashing == true)
            {
                //mFacingDirection *= 1;////new added
                mSpeed = mSpeed + 10;
                dashing = false;
                //Console.WriteLine("dashing is true");
            }

            if (distance >= 250)
            {
               // Console.WriteLine("dashing is false");
                dashing = false;
                behaviourdash = false;
                mSpeed = 1;
                //randomevent = true;
            }
            return tokenPosition;
        }
         public Vector3 Accelerate(Vector3 tokenPosition)////acceleration 
         {
               // DateTime oldtime = DateTime.Now;//////////////datetime now  
               // DateTime acceltime = DateTime.Now.AddSeconds(15);
                //currenttime = GetCurrentTime();

             /*if setonce - true then do that stuff
                 endTime = currentTime + 15;
                 Set setonce to false*/


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
                    if (mSpeed < 12)////////////
                    {
                        mSpeed += 1;///increment 1 by 1
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
               return tokenPosition;/////return always the last*/  
        }

       /* private Vector3 Hungry(Vector3 tokenPosition)
        {
            tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;///allows +=
            this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);
            //timer.Interval = 1000;
            timer.Interval = rnd.Next(1000, 3000);//////////HELPHELP
            timer.Start();/////start the timer     
        }*/
        
            public int gRandom(int min, int max)//////////////////generating number///////////
            {
                    Random rnd = new Random();
                    int Value = rnd.Next(1, 5);////generates from 1 to 4
                    return Value;///can u get this here??
                    //Random rtimer = new Random();///////////////////////////////////how to generate 2 random things in 1 random method
                    //timer.Interval = rnd.Next(1000, 3000);//////////cant random interval be created here
                    //mValue = pValue;
                    //grandom.rmd(x,x);
            }

            private int GetCurrentTime()////////////////////passed time method ??????
            {
                int currenttime = DateTime.Now.Second + DateTime.Now.Minute * 60;
                //Console.WriteLine("time passed" +  currenttime);
                return currenttime;
            }
                /*  public int Random(int min, int max)//////////////////generating number///////////
                  {
                      Random rnd = new Random();                               
                      int Value = rnd.Next(1, 6);
                     // mValue = pValue;
                      return Value;///can u get this here??
                  }
                     //Dash(tokenPosition)
                     //this.PossessedToken.Position = Dash(tokenPosition);*/
                              
    }
}


#endregion

  /* public Vector3 VerticalSwimBehaviour(Vector3 tokenPosition)
        {
   
            tokenPosition.Y = tokenPosition.Y + mSpeed * mFacingDirection;///do it so it wont go -4 speed allows to increase speed

            this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);

            if (tokenPosition.Y > 200 || tokenPosition.Y < -200)
            {
                DaSpeed();
                mFacingDirection *= -1;///
                turns++;

            }
            return tokenPosition;

        }*/
