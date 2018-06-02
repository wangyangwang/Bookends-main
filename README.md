This is the kinect prototye code for project _Bookends_

Project is for the technical demostration at late june in Shanghai



# Development Checklist


### Unity

- [ ] Avatars and Animation Controller
    - [x] Rigging (by 3d artists)
    - [ ] TEACHER/LEADER
    - [ ] FOLLOWER
    - [ ] Birds
    - [ ] More to come...

- [ ] Scene environment (Design and construct the environment where all actions take place.)

### Classes

- [ ] Kinect Integration
    - [x] Get skeleton data (`KinectManager`)
    - [x] Control avatar (`AvatarController`)
    - [ ] Gesture Recognizer

- [ ] `TimeController`

Control timings like when to cue a certain music, or when to start a certain animation, etc.

- [ ] `AudioController`



- [ ] `OSCHandler`

    - [ ] Receive OSC data from Richard's iPad control panel app
    - [ ] possibily send OSC out (as feedback?)

- [ ] `ParicleSystemController`

    - [ ] Profiles for Particle Effects.
    - [ ] Switch between profiles
