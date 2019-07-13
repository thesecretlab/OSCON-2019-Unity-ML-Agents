Warehouse Notes

The Warehouse demo is largely based on the PushBlocks demo.

## Source code overview

### BeepoAgent.cs

The agent. Largely based on the `PushAgentBasic` class. The main significant changes is that instead of tracking a single block and a single goal, the script looks for all blocks and goals that are part of the Area it's a child of.

When the agent's `IScoredAGoal` method is called, it checks to see if all of the blocks are inactive. If this is the case, the agent indicates that it's done by calling `Done`; it will also separately call 

### PushBlockCube.cs

The PushBlockCube.cs file checks to see if it has entered the trigger area of a `PushBlockGoal`. If the goal is of the same `type` as the block, the `agent`'s `IScoredAGoal` method is called, and the block becomes inactive by setting its `IsActive` property to `false`.

When a block is inactive, it sets its layer to the "Ignore Raycasts" layer, so that the AI can't see it, and then disables its game object. Setting the block's `IsActive` property to `true` will reverse these actions.

### PushBlockGoal.cs

This script manages the appearance of goals, and exists so that the `Agent` can call `SetColor` on it to set the colour of the goal appropriately.

### PushBlockAcademy.cs

Largely identical to the original, but also manages the list of colours used to tint each goal and block. See the `GoalType` enum and `GoalDefinition` class in this file.

## Creating Gameplay

The `Area` prefab is an independent object that you can create multiple copies of. To change the number of blocks and goals (max 4 each), simply open the prefab, delete Blocks and Goals as desired, and return to the scene. The agent will automatically detect blocks and goals that apply to it.