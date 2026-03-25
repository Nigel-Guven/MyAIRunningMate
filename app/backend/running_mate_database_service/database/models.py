from sqlalchemy import Column, String, Float, Integer, DateTime, ForeignKey
from sqlalchemy.orm import relationship
from database.base import Base

class Activity(Base):
    __tablename__ = "activities"

    id = Column(String, primary_key=True)
    date = Column(DateTime, nullable=False)
    type = Column(String, nullable=False)
    duration_seconds = Column(Float, nullable=False)
    distance_metres = Column(Float, nullable=False)
    average_heart_rate = Column(Integer)
    max_heart_rate = Column(Integer)
    total_elevation_gain = Column(Float)
    average_pace_seconds_per_kilometre = Column(Float)
    training_effect = Column(Float)
    vo2_max = Column(Float)

    laps = relationship("Lap", back_populates="activity", cascade="all, delete")


class Lap(Base):
    __tablename__ = "laps"

    id = Column(Integer, primary_key=True)
    activity_id = Column(String, ForeignKey("activities.id", ondelete="CASCADE"))
    lap_number = Column(Integer, nullable=False)
    distance_metres = Column(Float, nullable=False)
    duration_seconds = Column(Float, nullable=False)
    average_heart_rate = Column(Integer)

    activity = relationship("Activity", back_populates="laps")