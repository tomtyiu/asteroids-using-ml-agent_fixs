# ML-Agents Asteroids AI

In this project I used ML-Agents to create an AI for a game similar to Atari Asteroids. I succeeded in training an AI, but the results are far from satisfactory.
I have tried training the AI by giving it different sensors or even cheating by not using sensors at all, instead giving it parameters of the asteroids.
In the end a raycast perception sensor 2d worked best. But the AI, is still doomed to fail eventually, even though it usually reaches a fairly good score.

## Environment Rules

There are 2 types of possible objects in the game. The asteroids and the spaceship, which is the agent.
The asteroids are set to spawn in a random position around a square "safe zone", just so the asteroids would not spawn on top of the agent.
Asteroids can have one of three sizes, and depending on their size they take a different amount of projectiles to destroy.
An asteroid is given a velocity towards the middle of the map, with a random range in direction to make it unpredictable.
Object pooling is used for the asteroids.
The agent can rotate, move forwards and shoot. The agent is restrained to the map, if it goes out of bounds, the axis that went out of bounds is reversed.
The agent also has a limited movement, rotation, and attack speeds.

The game's environment is not an exact copy of Asteroids. For example, an out of bounds asteroid is destroyed, whereas in Atari's version, it the coordinate axis for the asteroid is reversed.
And I didn't even try to get the values exactly right.

## Rewards

The agent gets a small reward for time elapsed, because the main purpose of the agent is to last as long as possible.
The agent has a small negative reward for shooting, but is rewarded for destroying asteroids. This works to prevent the agent from spamming shoot non-stop
and encourages the agent to aim precisely. The agent is also given a negative reward for going out of bounds of the map,
because I noticed that this was a common pitfall in training. Agents would go near the edge of the map, and would not have enough time to react to the spawned asteroids.
And lastly, agents are of course given a negative reward for colliding with an asteroid.

## Agent Actions and Sensors

There are 3 discrete Vector action branches being observed.
1st is rotation, rotate left, rotate right, do nothing.
2nd is shooting, shoot, do nothing.
3rd is Movement, move forwards, do nothing.

The agent makes 5 vector observations with no stacking.
Velocity, x and y.
Position, x and y.
And its z rotation.

I have considered stacking the vectors and removing the velocity observation, this would allow velocity to be inferred by the agent from the difference in stacked values.
But by stacking the vectors I would also make the agent observe its angular rotation (because it would see the difference between previous and current z rotation).
I do not think angular rotation is important for the agent to learn, so by giving it its velocity, I think the agent would learn faster.

The agent uses 20 Ray Casts to see the asteroids so it could dodge them and aim at them. This sensor yielded the best results. I have tried reducing the number of raycasts,
but that puts even more blind spots in the agent's defense. The agent tries to compensate for it by spinning more. By spinning the agent can move the raycasts around,
and compensate for the blind spots, but I found 20 raycasts to be the sweet spot between learning time and blind spots. Constant agent rotation is also not something I desired.

The ray cast perception sensor though does not fit my situation ideally though. [I wrote more about the issue in this Unity forum thread](https://forum.unity.com/threads/agent-perception-in-2d.1009399/) where I tried to find help.
In short, if there is an asteroid behind an asteroid that the raycast hits, the agent has no way of seeing it and that asteroid can catch the agent "off guard".

This problem made me look into another possibility to allow the agent to "see" the asteroids. My idea was to use object pooling for the asteroids,
add the asteroids' positions and whether the asteroid object is active or not, to the agent's observational parameters. This method would not have the same problem I talked about
above which the ray perception sensor does. However, the training didn't work at all and I couldn't figure out why in the end.

## Results

So these are the results.

![Results](https://i.imgur.com/QO8DE7V.png "Results")

Graph shows the length of training episodes, with smoothing of 0.6.
Ultimately, all of the runs suffered from the same problem, the results would gradually increase, but would also start to get more and more erratic,
reducing the reliability of the AI.

## Credits

Spasheship by MillionthVector (http://millionthvector.blogspot.de)

Asteroids by Jasper on OpeGameArt.org

Background courtesy NASA/JPL-Caltech, Public domain, via Wikimedia Commons
