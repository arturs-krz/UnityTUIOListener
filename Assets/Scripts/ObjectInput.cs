using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInput
{
    // Temporary ID for the surface object
    // we need this to keep track if it's active
    public int id { get; }

    // The actual marker id
    public int tagValue { get; }

    public Vector2 position { get; private set; }

    public float orientation { get; private set; }
    public Vector2 velocity { get; private set; }
    public float acceleration { get; private set; }

    public float angularVelocity { get; private set; }

    public float angularAcceleration { get; private set; }

    public ObjectInput(int id, int tagValue, Vector2 position, float orientation, Vector2 velocity, float acceleration, float angularVelocity, float angularAcceleration)
    {
        this.id = id;
        this.tagValue = tagValue;
        this.position = position;
        this.orientation = orientation;
        this.velocity = velocity;
        this.acceleration = acceleration;
        this.angularVelocity = angularVelocity;
        this.angularAcceleration = angularAcceleration;
    }

    public void UpdateProps(Vector2 position, float orientation, Vector2 velocity, float acceleration, float angularVelocity, float angularAcceleration) {
        this.position = position;
        this.orientation = orientation;
        this.velocity = velocity;
        this.acceleration = acceleration;
        this.angularVelocity = angularVelocity;
        this.angularAcceleration = angularAcceleration;
    }
}