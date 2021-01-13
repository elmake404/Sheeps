Flock Simulator
(c) 2013 by Blossom Games -- http://blossom-games.com/

README



1. Introduction

The goal of this project was to implement behaviour of animals like fishes,
birds who move in groups (flocks, schools, herds etc.) in Unity 3D.

We decided to implement concepts described by Craig Reynolds in his "Boids"
project (http://www.red3d.com/cwr/boids/).

Take a look at the YouTube demonstration at
http://www.youtube.com/watch?v=pWysWFxitos.



2. Basic Concepts

Flock members (represented by the FlockMember component) are grouped into
groups (represented by the FlockGroup component). FlockGroup is a component
that should be attached to a dummy game object. Each FlockMember holds a
reference to its current FlockGroup. 

Properties of a FlockGroup are grouped into four categories that represent
steering behaviour of the flock. They are
 - alignment: steer towards the average heading of local flockmates,
 - separation: steer to avoid crowding local flockmates,
 - cohesion: steer to move toward the average position of local flockmates,
 - random: steer randomly.

Tweaking FlockGroup properties can change behaviour of its members entirely.
It is recommended to choose the parameters experimentally.

The FlockGroup fills its FlockMembers' public flockOutput fields that
represent where each flock member would like to go according to its herd
instinct.

PLEASE NOTE THAT FlockSimulator components don't actually move any objects,
just express the desire of every flock member to go in a given direction.
It's up to the programmer to use flockOutput to obtain the result that is
expected. Wedecided to make it that way to ensure flexibility.



3. Public Properties

FlockMember:
 - flockGroup -- reference to the component that represents the flock group.
 - gatherVelocityAutomaticallyPeriod -- every flock member needs to know
   its velocity. A flock member calculates it automatically, periodically by
   tracing its global position. Given parameter describes the period in
   seconds by which such computation occures. Default value is 0.1, change
   it to bigger values in order to trade off the quality of the simulation for
   some performance.
 - flockOutput -- vector the programmer shoud read and use to move the object.

FlockGroup:
 - quality -- float greater than 0 and not greater than 1 that describes the
   quality of the simulation. Default value is 1. Decrease this value if you
   want to trade off simulation quality for some performance.

 - alignment -- bool that specifies if the alignment steering behaviour is
   enabled.
 - alignmentWeight -- specifies how important alignment is.
 - alignmentAlgorithm -- describes how alignment algorithm works, i.e.
   how distance to an other flock member affects the outcome:
   - Linear -- the influence linearly decreases,
   - ByCurve -- the influence is described by a curve. It should probably
     be decreasing. The x coordinate of the curve is a distance to the other
     member whereas the y coordinate is the influence factor for the given
     distance.

 - separation -- bool that specifies if the separation steering behavior is
   enabled.
 - separationWeight -- specifies how important separation is.
 - separationAlgorithm -- describes how separation algorithm works. It works
   similarly to the alignmentAlgorithm parameter.

 - cohesion -- bool that specifies if the cohesion steering behavior is
   enabled.
 - cohesionMaxDistance -- specifies radius where flockmates are treated as
   local and taken into account in the cohesion steering behaviour.
 - cohesionWeight -- specifies how important cohesion is.
 - cohesionAlgorithm -- describes how cohesion algorithm works. It works
   similarly to the alignmentAlgorithm parameter.

 - random -- bool that specifies if the random steering behaviour is enabled.
 - randomWeight -- specifies how important random is.
 - randomPeriod -- time given in seconds by which every flock member changes
   its random direction.



4. Examples

FlockSimulator comes with four examples:
 - FlockMemberExample -- shows how one can use flockOutput to move the object
   (see FlockMemberMoveExample.cs and how components are attached to game
   objects),
 - FlockMemberRigidBodiesExample -- shows how one can use flockOutput to steer
   objects with RigidBody components,
 - FlockLeaderPredatorsExample -- an example of a simple game with two flocks
   -- one that runs away and second that chases the other one,
 - FlockPredators3DExample -- shows that 3D is not a problem.



5. Contact

If you have any questions, do not hesitate to ask at support@blossom-games.com.
