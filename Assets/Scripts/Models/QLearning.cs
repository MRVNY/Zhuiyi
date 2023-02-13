using Numpy;
using static System.Math;
using System.Collections.Generic;

public class QLearning {

    private NDarray QTable;

    public QLearning(int nbStates, int nbActions){
        // Initialize the Q-table with zeros
        this.QTable = np.zeros((nbStates, nbActions));
    }

    public QLearning(NDarray QTable){
        this.QTable = QTable;
    }

    public QLearning(string filePath){
        this.QTable = np.load(filePath);
    }

    public void SaveToFile(string filePath){
        np.savetxt(filePath, QTable);
    }

    public int getNextAction(int state, List<string> availableActions, float epsilon = 0){
        return eGreedy(state, epsilon);
    }

    public void updateQTable(int old_state, int new_state, int action, float reward, float alpha, float gamma){
        QTable[$"{old_state}, {action}"] = (1-alpha) * QTable[$"{old_state}, {action}"] + alpha * (reward + gamma * QTable[$"{new_state}, :"].max());
    }

    // Returns an action following the epsilon-greedy exploration policy
    // Inputs :
    // - state: the state for which we want the action
    // - epsilon: rate of random actions
    // Output :
    // - u: chosen action
    public int eGreedy(int state, float epsilon){
    
        if (np.random.rand() < epsilon) {
            return (int) np.random.randint(QTable[$"{state}"].size);
        } else {
            // choose randomly between the actions with a max value
            List<float> tmp = new List<float>();
            for (int i = 0; i < QTable[$"{state}, :"].size; i++){
                if (QTable[$"{state}, {i}"] == QTable[$"{state}, :"].max()){
                    tmp.Add(i);
                }
            }

            return (int) np.random.choice(np.array(tmp.ToArray()))[0];
        }
    }
}