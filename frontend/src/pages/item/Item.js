import React, { useState, useEffect } from 'react'
import './styles.scss'
import { withRouter } from 'react-router-dom'
import ImagePanel from '../../layouts/imagePanel/ImagePanel'
import SummaryPanel from '../../layouts/summaryPanel/SummaryPanel'
import axios from 'axios'

const Item = ({ match }) => {
	
	const id = match.params.id;
	
	const [item, setItem] = useState(null);

	useEffect(() => {
		fetchItems();
	});

	async function fetchItems(){
		const BASE_URL = "https://localhost:5001/api/items";
		const url = `${BASE_URL}/${id}`;
		const response = await axios.get(url);
		const item = response.data;
		setItem(item);
	}

	return (
		<div className='item-page container-fluid'>
			{	item ? ( 
				<React.Fragment>
					<ImagePanel imageUrls={item.imageUrl} />
					<SummaryPanel item={item} />
				</React.Fragment>
			) : null
			} 
		</div>
	)
}

export default withRouter(Item);
