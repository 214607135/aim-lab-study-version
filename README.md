# aim-lab-study-version

  an easy version of FPS shoot game, trying to copy the same mode from aim lab game which is famous in steam.

  ##### 2022.08.09 check-in:
  	basic functionality has been completed.
	Todo: add visual effect such as:
		1. The ball should be splited if it is shot
		2. Add shooting sound and hand animation

  ##### 2022.08.10 check-in
  	Add ball split effect by using tool blender, but have many bugs currently.
		1. After the ball is hit and splited, it cannot vanished currently.
		2. If the ball segment touches other balls, it will make them split also.
	Todo:
		1. Made ball segment vanished after 1 or 2 seconds.
		2. Do not give balls rigidbody component before they are splited, it may help solve bug 2.
		3. Or, directly learn how to create animation in blender software?

  ##### 2022.08.15 check-in
  	Delete split effect, it would cause code less-readable and difficult to modify.
	Todo:
		1. Create split animation, however, I do not know how to do it yet.
		2. Or, just made a video of current game mode first, can do the animation later.
