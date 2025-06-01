pipeline {

    agent any
    
    stages {

        stage('Deploy FE') {
            steps {
                echo 'Deploying and cleaning'
            }
        }
        
 
    }
    post {
        // Clean after build
        always {
            cleanWs()
        }
    }
}
