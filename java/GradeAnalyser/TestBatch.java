package GradeAnalyser;

import java.util.ArrayList;

public class TestBatch {
	
	private String name;
	private ArrayList<Test> batch;
	
	public TestBatch(String name)
	{
		this.name = name;
		batch = new ArrayList<Test>();
	}
	
	public void add(Test add)
	{
		batch.add(add);
	}
	
	public double totalAverage()
	{
		
		double totalScore = 0;
		
		for(Test T : batch)
		{
			totalScore += T.getAverage();
		}
		
		return totalScore/batch.size();
	}
	
	public double questionAverage(int index)
	{
		
		double totalScore = 0;
		
		for(Test T : batch)
		{
			totalScore += T.getAnswerAt(index);
		}
		
		return 100.0*(totalScore/batch.size());
		
		
	}
	
	public Test getTestAt(int index)
	{
		
		return batch.get(index);
	}
	public int size()
	{
		return batch.size();
	}
	public ArrayList<Test> getBatch()
	{
		return batch;
		
	}
	

}
