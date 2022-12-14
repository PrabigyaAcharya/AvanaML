from flask import Flask, render_template, url_for, request, jsonify
import pandas as pd
import pickle
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.naive_bayes import MultinomialNB
import sklearn.externals
import joblib
import pickle
from collections.abc import Mapping


# load the model from disk
filename = 'nlp_model.pkl'
clf = pickle.load(open(filename, 'rb'))
cv = pickle.load(open('tranform.pkl', 'rb'))
app = Flask(__name__)


@app.route('/', methods=['GET'])
def home():
    return render_template('home.html')


# @app.route('/predict', methods=['POST'])
# def predict():
#     if request.method == 'POST':
#         message = request.form['message']
#         data = [message]
#         vect = cv.transform(data).toarray()
#         my_prediction = clf.predict(vect)
#     return render_template('result.html', prediction=my_prediction)


@app.route('/predict/<text>', methods=['GET'])
def recommend(text):
    data = [text]
    vect = cv.transform(data).toarray()
    my_prediction = clf.predict(vect)
    if my_prediction == 1:
        return jsonify({'prediction': "Spam"})
    else:
        return jsonify({'prediction': "Not Spam"})


if __name__ == '__main__':
    app.run(debug=True)
