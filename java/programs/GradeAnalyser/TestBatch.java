package programs.GradeAnalyser;

import java.util.ArrayList;

/*

	TestBatch
	
	a batch of tests
	
	can find the total average of all the tests, 
	or the average of a single question across all tests (assuming the same amount of questions)

*/

public class TestBatch {
	
	private ArrayList<Test> batch;
	
	public TestBatch(String name)
	{
		batch = new ArrayList<Test>();
	}
	
	public void add(Test add)
	{
		batch.add(add);
	}
	
	/*
	
		totalAverage
		
		averages score of all tests, returns average
	
	*/
	public double totalAverage()
	{
		
		double totalScore = 0;
		
		for(Test T : batch)
		{
			totalScore += T.getAverage();
		}
		
		return totalScore/batch.size();
	}
	
	/*
	
	
		questionAverage
		
		input - index of question
		
		finds and returns average of a selected question across all tests in the batch
	*/
	
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
