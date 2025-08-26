from sqlalchemy import Column, BigInteger, Integer, String, Float, DateTime, ForeignKey, Text
from sqlalchemy.orm import relationship
from .database import Base
from datetime import datetime

class Activity(Base):
    __tablename__ = "activities"

    id = Column(BigInteger, primary_key=True, index=True)
    name = Column(String, nullable=True)
    distance = Column(Float, nullable=True)
    moving_time = Column(Float, nullable=True)
    elapsed_time = Column(Float, nullable=True)
    elevation_gain = Column(Float, nullable=True)
    type = Column(String, nullable=True)
    start_date = Column(DateTime, nullable=True)
    start_date_local = Column(DateTime, nullable=True)
    average_speed = Column(Float, nullable=True)
    max_speed = Column(Float, nullable=True)
    average_cadence = Column(Float, nullable=True)
    average_watts = Column(Float, nullable=True)
    max_watts = Column(Float, nullable=True)
    weighted_average_watts = Column(Float, nullable=True)
    kilojoules = Column(Float, nullable=True)

    insights = relationship("Insight", back_populates="activity")

class Insight(Base):
    __tablename__ = "insights"

    id = Column(BigInteger, primary_key=True, index=True)
    activity_id = Column(Integer, ForeignKey("activities.id"), nullable=True)
    insight = Column(Text, nullable=False)
    created_at = Column(DateTime, default=datetime.now)

    activity = relationship("Activity", back_populates="insights")
